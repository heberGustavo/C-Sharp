using AutoMapper;
using Dapper;
using Dapper.Contrib.Extensions;
using Sorteio.Common;
using Sorteio.Data.EntityData;
using Sorteio.Data.Repository.Base;
using Sorteio.Domain.IRepository;
using Sorteio.Domain.Models.Body;
using Sorteio.Domain.Models.Common;
using Sorteio.Domain.Models.EntityDomain;
using Sorteio.Domain.Models.NotMapped;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorteio.Data.Repository
{
    public class SorteiosRepository : RepositoryBase<Domain.Models.EntityDomain.Sorteio, Data.EntityData.SorteioData>, ISorteiosRepository
    {

        public SorteiosRepository(SqlDataContext dataContext, IMapper mapper) : base(dataContext, mapper)
        {
        }

        public Task<IEnumerable<NumeroEscolhidoBody>> BuscarNumerosReservadoOuPagoSorteioPorId(int idSorteio, int idStatusPedido)
            => _dataContext.Connection.QueryAsync<NumeroEscolhidoBody>(@"SELECT ne.*, p.id_status_pedido, u.nome as nome_usuario
                                                                         FROM NumeroEscolhido ne 
                                                                         LEFT JOIN Pedido p ON p.id_pedido = ne.id_pedido 
                                                                         LEFT JOIN Sorteio s ON s.id_sorteio = p.id_sorteio 
                                                                         LEFT JOIN Usuario u ON u.id_usuario = p.id_usuario 
                                                                         WHERE s.id_sorteio = @idSorteio AND p.id_status_pedido = @idStatusPedido AND p.status = 0
                                                                         ORDER BY ne.numero",
                                                                         new { idSorteio, idStatusPedido });

        public Task<IEnumerable<NumeroEscolhidoBody>> BuscarTodosNumerosSorteioPorId(int idSorteio)
            => _dataContext.Connection.QueryAsync<NumeroEscolhidoBody>(@"SELECT ne.*, p.id_status_pedido, u.nome as nome_usuario
                                                                         FROM NumeroEscolhido ne 
                                                                         LEFT JOIN Pedido p ON p.id_pedido = ne.id_pedido 
                                                                         LEFT JOIN Sorteio s ON s.id_sorteio = p.id_sorteio 
                                                                         LEFT JOIN Usuario u ON u.id_usuario = p.id_usuario 
                                                                         WHERE s.id_sorteio = @idSorteio AND p.status = 0
                                                                         ORDER BY ne.numero", new { idSorteio });

        public async Task<bool> CadastrarNumerosEscolhidos(decimal valorTotal, IEnumerable<NumeroEscolhido> numeroSorteios, int idUsuario, int idSorteio)
        {
            using (var dbContextTransaction = _dataContext.Connection.BeginTransaction())
            {
                try
                {
                    var pedidoData = new PedidoData()
                    {
                        id_usuario = idUsuario,
                        id_sorteio = idSorteio,
                        data_pedido = DateTime.Now,
                        valor_total = valorTotal,
                        id_status_pedido = DataDictionary.STATUS_PEDIDO_PENDENTE
                    };

                    var idPedidoCadastrado = await _dataContext.Connection.InsertAsync(pedidoData, dbContextTransaction);

                    foreach (var itemNumeros in numeroSorteios)
                    {

                        var verificarSeNumeroJaEscolhido = _dataContext.Connection.ExecuteScalar<int>(@"SELECT COUNT(1) 
                                                                                                        FROM NumeroEscolhido NE
                                                                                                        LEFT JOIN Pedido p ON NE.id_pedido = p.id_pedido
                                                                                                        LEFT JOIN StatusPedido sp ON p.id_status_pedido = sp.id_status_pedido
                                                                                                        WHERE p.id_sorteio = @id_sorteio AND ne.numero = @numero AND sp.id_status_pedido != @STATUS_PEDIDO_CANCELADO; ",
                                                                                                        new { id_sorteio = pedidoData.id_sorteio, numero = itemNumeros.numero, STATUS_PEDIDO_CANCELADO = DataDictionary.STATUS_PEDIDO_CANCELADO }, dbContextTransaction);

                        if (verificarSeNumeroJaEscolhido == 0)
                        {
                            var numeroEscolhidoData = new NumeroEscolhidoData
                            {
                                id_pedido = idPedidoCadastrado,
                                numero = itemNumeros.numero
                            };

                            await _dataContext.Connection.InsertAsync(numeroEscolhidoData, dbContextTransaction);
                        }
                        else
                            return false;
                    }

                    dbContextTransaction.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    return false;
                }
            }
        }

        public Task<int> ConfirmarPagamentoRecebido(int idPedido, int idStatusPago)
            => _dataContext.Connection.ExecuteAsync(@"UPDATE Pedido SET id_status_pedido = @idStatusPago WHERE id_pedido = @idPedido", new { idStatusPago, idPedido });

        public async Task<ResultResponseModel> CriarNovoSorteio(SorteioBody sorteioBody)
        {
            using (var dbContextTransaction = _dataContext.Connection.BeginTransaction())
            {
                try
                {
                    var idSorteioCadatrado = await _dataContext.Connection.InsertAsync(_mapper.Map<SorteioData>(sorteioBody.sorteio), dbContextTransaction);

                    foreach (var itemImagem in sorteioBody.linkImagens)
                    {
                        itemImagem.id_sorteio = idSorteioCadatrado;

                        await _dataContext.Connection.InsertAsync(_mapper.Map<GaleriaFotosData>(itemImagem), dbContextTransaction);
                    }

                    dbContextTransaction.Commit();

                    return new ResultResponseModel(false, "Sorteio cadastrado com sucesso");
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    return new ResultResponseModel(true, "Erro ao cadastrar Sorteio, entre em contato com o suporte.");
                }
            }
        }

        public async Task<bool> EditarFinalizarSorteio(VencedorSorteio body)
            => await _dataContext.Connection.UpdateAsync(_mapper.Map<VencedorSorteioData>(body));

        public async Task<ResultResponseModel> EditarSorteio(SorteioBody body)
        {
            using (var dbContextTransaction = _dataContext.Connection.BeginTransaction())
            {
                try
                {
                    await _dataContext.Connection.UpdateAsync(_mapper.Map<SorteioData>(body.sorteio), dbContextTransaction);

                    if (body.linkImagens.Count() > 0) //Se existir imagem para esse sorteio, apaga todas antes de cadastrar as novas
                    {
                        await _dataContext.Connection.ExecuteAsync(@"DELETE FROM GaleriaFotos WHERE id_sorteio = @idSorteio", new { idSorteio = body.sorteio.id_sorteio }, dbContextTransaction);
                    }

                    foreach (var itemImagem in body.linkImagens)
                    {
                        itemImagem.id_sorteio = body.sorteio.id_sorteio;

                        await _dataContext.Connection.InsertAsync(_mapper.Map<GaleriaFotosData>(itemImagem), dbContextTransaction);
                    }

                    dbContextTransaction.Commit();

                    return new ResultResponseModel(false, "Sorteio atualizado com sucesso!");
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    return new ResultResponseModel(true, "Erro ao atualizar Sorteio, entre em contato com o suporte.");
                }
            }
        }

        public Task<int> ExcluirSorteio(int idSorteio)
            => _dataContext.Connection.ExecuteAsync(@"UPDATE Sorteio SET excluido = 1 WHERE id_sorteio = @idSorteio", new { idSorteio });

        public Task<IEnumerable<InformacoesSorteio>> FiltrarSorteioPorCategoria(int idCategoria)
             => _dataContext.Connection.QueryAsync<InformacoesSorteio>(@"SELECT s.id_sorteio, s.nome, s.edicao, s.valor, s.quantidade_numeros, s.status, 
                                                                        vs.numero_sorteado, vs.data_sorteio, 
                                                                        u.nome as nome_ganhador, 
                                                                        (
	                                                                        SELECT TOP 1 gf.url_imagem 
	                                                                        FROM GaleriaFotos gf  
	                                                                        WHERE gf.id_sorteio = s.id_sorteio 
	                                                                        ORDER BY gf.url_imagem
                                                                        ) as url_imagem 
                                                                        FROM Sorteio s 
                                                                        LEFT JOIN VencedorSorteio vs ON s.id_sorteio = vs.id_sorteio 
                                                                        LEFT JOIN Usuario u ON vs.id_usuario = u.id_usuario
                                                                        WHERE s.id_categoria_sorteio = @idCategoria AND s.status = 1 AND s.excluido = 0
                                                                        ORDER BY s.edicao + 0 ASC",
                                                                        new { idCategoria });

        public async Task<ResultResponseModel> FinalizarSorteio(VencedorSorteio vencedorSorteio)
        {
            using (var dbContextTransaction = _dataContext.Connection.BeginTransaction())
            {
                try
                {
                    var idVencedorSorteioCadastrado = await _dataContext.Connection.InsertAsync(_mapper.Map<VencedorSorteioData>(vencedorSorteio), dbContextTransaction);

                    await _dataContext.Connection.ExecuteAsync(@"UPDATE Sorteio
                                                                 SET status = 1
                                                                 WHERE id_sorteio = @idSorteio",
                                                                 new { idSorteio = vencedorSorteio.id_sorteio },
                                                                 dbContextTransaction);

                    dbContextTransaction.Commit();

                    return new ResultResponseModel(false, "Sorteio finalizado com sucesso");
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    return new ResultResponseModel(true, "Erro ao finalizar Sorteio, entre em contato com o suporte.");
                }
            }
        }

        public async Task<SorteioBody> ObterDadosDoSorteioPorId(int idSorteio)
        {
            try
            {
                var results = _dataContext.Connection.QueryMultiple(@"
                                                                        SELECT * FROM Sorteio s WHERE s.id_sorteio = @idSorteio
                                                                        SELECT * FROM GaleriaFotos gf WHERE gf.id_sorteio = @idSorteio
                                                                     ", new { idSorteio });

                var sorteio = results.ReadSingleOrDefault<Domain.Models.EntityDomain.Sorteio>();
                var imagens = results.Read<GaleriaFotos>();

                var dados = new SorteioBody(sorteio, imagens);

                return dados;

            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Task<IEnumerable<InformacoesSorteio>> ObterInformacoesSorteio()
            => _dataContext.Connection.QueryAsync<InformacoesSorteio>(@"SELECT s.id_sorteio, s.nome, s.edicao, s.valor, s.quantidade_numeros, s.status,
                                                                        vs.numero_sorteado, vs.data_sorteio, 
                                                                        u.nome as nome_ganhador,
                                                                        (SELECT TOP 1 gf.url_imagem 
                                                                            FROM GaleriaFotos gf  
                                                                            WHERE gf.id_sorteio = s.id_sorteio 
                                                                            ORDER BY gf.url_imagem) as url_imagem,
                                                                        (SELECT COUNT(1) 
	                                                                        FROM NumeroEscolhido ne 
	                                                                        LEFT JOIN Pedido p ON p.id_pedido = ne.id_pedido 
	                                                                        WHERE p.id_sorteio = s.id_sorteio and p.id_status_pedido = 1 
                                                                        ) as numeros_reservados,
                                                                        (SELECT COUNT(1) 
	                                                                        FROM NumeroEscolhido ne 
	                                                                        LEFT JOIN Pedido p ON p.id_pedido = ne.id_pedido 
	                                                                        WHERE p.id_sorteio = s.id_sorteio and p.id_status_pedido = 2 AND p.status = 0
                                                                        ) as numeros_pagos
                                                                        FROM Sorteio s
                                                                        LEFT JOIN VencedorSorteio vs ON s.id_sorteio = vs.id_sorteio 
                                                                        LEFT JOIN Usuario u ON vs.id_usuario = u.id_usuario
                                                                        WHERE s.excluido = 0
                                                                        ORDER BY s.edicao + 0 ASC");

        public Task<IEnumerable<MeusPremios>> ObterMeusPremiosClientePorId(int idUsuario)
            => _dataContext.Connection.QueryAsync<MeusPremios>(@"SELECT s.nome as nome_sorteio, vs.data_sorteio, vs.numero_sorteado 
                                                                 FROM VencedorSorteio vs 
                                                                 LEFT JOIN Sorteio s ON vs.id_sorteio = s.id_sorteio 
                                                                 WHERE vs.id_usuario = @idUsuario
                                                                 ORDER BY vs.data_sorteio DESC", new { idUsuario });

        public Task<IEnumerable<NumeroEscolhidoBody>> ObterNumerosDoSorteioPorId(int idSorteio)
            => _dataContext.Connection.QueryAsync<NumeroEscolhidoBody>(@"SELECT ne.*, p.id_status_pedido, u.nome as nome_usuario 
                                                                         FROM Pedido p 
                                                                         LEFT JOIN NumeroEscolhido ne ON p.id_pedido = ne.id_pedido 
                                                                         LEFT JOIN Usuario u ON u.id_usuario = p.id_usuario 
                                                                         WHERE p.id_sorteio = @idSorteio AND p.status = 0 AND p.id_status_pedido != 4", new { idSorteio });

        public Task<IEnumerable<ParticipanteSorteio>> ObterParticipantesSorteioPorId(int idSorteio)
            => _dataContext.Connection.QueryAsync<ParticipanteSorteio>(@"SELECT u.id_usuario, u.nome, p.id_status_pedido, p.id_pedido
                                                                         FROM Usuario u 
                                                                         LEFT JOIN Pedido p ON u.id_usuario = p.id_usuario 
                                                                         LEFT JOIN NumeroEscolhido ne ON p.id_pedido = ne.id_pedido
                                                                         WHERE p.id_sorteio = @idSorteio
                                                                         GROUP BY u.id_usuario, u.nome, p.id_status_pedido, p.id_pedido;", new { idSorteio });

        public async Task<SorteioNotMapped> ObterSorteioPorId(int idSorteio)
            => await _dataContext.Connection.QueryFirstOrDefaultAsync<SorteioNotMapped>(@"SELECT COUNT(gf.id_galeria_fotos) quantidade_imagens, s.id_sorteio, s.id_categoria_sorteio, s.nome as nome_sorteio, s.edicao as edicao_sorteio, s.valor, s.quantidade_numeros, s.descricao_curta, s.descricao_longa, s.status,  
                                                                                          vs.id_usuario, vs.id_vencedor_sorteio, u.nome as nome_ganhador, vs.numero_sorteado, vs.data_sorteio
                                                                                          FROM Sorteio s
                                                                                          LEFT JOIN VencedorSorteio vs ON s.id_sorteio = vs.id_sorteio 
                                                                                          LEFT JOIN Usuario u ON vs.id_usuario = u.id_usuario
                                                                                          LEFT JOIN GaleriaFotos gf ON s.id_sorteio = gf.id_sorteio 
                                                                                          WHERE s.id_sorteio = @idSorteio
                                                                                          GROUP BY s.id_sorteio, s.id_categoria_sorteio, s.nome, s.edicao, s.valor, s.quantidade_numeros, s.descricao_curta, s.descricao_longa, s.status,  
                                                                                          vs.id_usuario, vs.id_vencedor_sorteio, u.nome, vs.numero_sorteado, vs.data_sorteio", new { idSorteio = idSorteio });

        public Task<IEnumerable<MeusBilhetes>> ObterSorteiosBilheteClientePorId(int idUsuario)
            => _dataContext.Connection.QueryAsync<MeusBilhetes>(@"SELECT p.id_pedido, s.nome as nome_sorteio, p.data_pedido as data_compra_sorteio, p.id_status_pedido, ne.numero, s.valor 
                                                                  FROM Pedido p 
                                                                  LEFT JOIN Sorteio s ON p.id_sorteio = s.id_sorteio
                                                                  LEFT JOIN NumeroEscolhido ne ON ne.id_pedido = p.id_pedido
                                                                  WHERE p.id_usuario = @idUsuario
                                                                  ORDER BY p.id_pedido ASC", new { idUsuario });

        public Task<IEnumerable<Pedido>> ObterTodosPedidosPendentes(int statusPendente)
        {
            try
            {
                var query = @"SELECT * 
                          FROM Pedido p 
                          WHERE p.id_status_pedido = @statusPendente AND status = 0;";

                return _dataContext.Connection.QueryAsync<Pedido>(query, new { statusPendente });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }

        }

        public Task<IEnumerable<SorteioNotMapped>> ObterTodosSorteio()
            => _dataContext.Connection.QueryAsync<SorteioNotMapped>(@"SELECT s.id_sorteio, u.id_usuario, s.nome as nome_sorteio, s.edicao as edicao_sorteio, s.status, u.nome as nome_ganhador
                                                                      FROM Sorteio s 
                                                                      LEFT JOIN VencedorSorteio vs ON s.id_sorteio = vs.id_sorteio 
                                                                      LEFT JOIN Usuario u ON vs.id_usuario = u.id_usuario
                                                                      WHERE s.excluido = 0
                                                                      ORDER BY s.edicao + 0 ASC");

        public Task<IEnumerable<InformacoesSorteio>> ObterTodosUltimosSorteiosRealizados()
            => _dataContext.Connection.QueryAsync<InformacoesSorteio>(@"SELECT s.id_sorteio, s.nome, s.edicao, s.valor, s.quantidade_numeros, s.status, 
                                                                        vs.numero_sorteado, vs.data_sorteio, 
                                                                        u.nome as nome_ganhador,
                                                                        (
	                                                                        SELECT TOP 1 gf.url_imagem 
	                                                                        FROM GaleriaFotos gf 
	                                                                        WHERE gf.id_sorteio = s.id_sorteio 
	                                                                        ORDER BY gf.url_imagem ASC 
                                                                        ) as url_imagem
                                                                        FROM Sorteio s 
                                                                        LEFT JOIN VencedorSorteio vs ON s.id_sorteio = vs.id_sorteio 
                                                                        LEFT JOIN Usuario u ON vs.id_usuario = u.id_usuario
                                                                        WHERE s.status = 1 AND s.excluido = 0
                                                                        ORDER BY s.edicao + 0 ASC");

        public async Task<int> RemoverPedidoPendenteAposPrazoMaximo(Pedido pedido, int statusCancelado)
        {
            using (var dbContextTransaction = _dataContext.Connection.BeginTransaction())
            {
                try
                {
                    await _dataContext.Connection.ExecuteAsync(@"UPDATE Pedido
                                                                 SET status = 1
                                                                 WHERE id_pedido = @idPedido;",
                                                                 new { idPedido = pedido.id_pedido }, dbContextTransaction);

                    await _dataContext.Connection.ExecuteAsync(@"UPDATE Pedido
                                                                 SET id_status_pedido = @statusCancelado
                                                                 WHERE id_pedido = @idPedido;",
                                                                 new { statusCancelado, idPedido = pedido.id_pedido }, dbContextTransaction);

                    dbContextTransaction.Commit();
                    return 1;
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    return 0;
                }
            }
        }

        public Task<IEnumerable<NumeroEscolhido>> VisualizarNumerosPorIdPedido(int idPedido)
            => _dataContext.Connection.QueryAsync<NumeroEscolhido>(@"SELECT * 
                                                                     FROM NumeroEscolhido ne 
                                                                     WHERE ne.id_pedido = @idPedido
                                                                     ORDER BY ne.numero", new { idPedido });
    }
}