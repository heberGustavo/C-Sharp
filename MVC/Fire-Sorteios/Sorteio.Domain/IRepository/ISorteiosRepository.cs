using Sorteio.Domain.IRepository.Base;
using Sorteio.Domain.Models.Body;
using Sorteio.Domain.Models.Common;
using Sorteio.Domain.Models.EntityDomain;
using Sorteio.Domain.Models.NotMapped;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sorteio.Domain.IRepository
{
    public interface ISorteiosRepository : IRepositoryBase<Models.EntityDomain.Sorteio>
    {
        Task<ResultResponseModel> CriarNovoSorteio(SorteioBody sorteioBody);
        Task<IEnumerable<SorteioNotMapped>> ObterTodosSorteio();
        Task<ResultResponseModel> FinalizarSorteio(VencedorSorteio vencedorSorteio);
        Task<SorteioNotMapped> ObterSorteioPorId(int idSorteio);
        Task<bool> EditarFinalizarSorteio(VencedorSorteio body);
        Task<ResultResponseModel> EditarSorteio(SorteioBody body);
        Task<IEnumerable<InformacoesSorteio>> ObterInformacoesSorteio();
        Task<IEnumerable<InformacoesSorteio>> FiltrarSorteioPorCategoria(int idCategoria);
        Task<IEnumerable<InformacoesSorteio>> ObterTodosUltimosSorteiosRealizados();
        Task<SorteioBody> ObterDadosDoSorteioPorId(int idSorteio);
        Task<bool> CadastrarNumerosEscolhidos(decimal valorTotal, IEnumerable<NumeroEscolhido> numeroSorteios, int idUsuario, int idSorteio);
        Task<IEnumerable<MeusBilhetes>> ObterSorteiosBilheteClientePorId(int id_usuario);
        Task<IEnumerable<MeusPremios>> ObterMeusPremiosClientePorId(int id_usuario);
        Task<IEnumerable<NumeroEscolhidoBody>> ObterNumerosDoSorteioPorId(int idSorteio);
        Task<IEnumerable<ParticipanteSorteio>> ObterParticipantesSorteioPorId(int idSorteio);
        Task<int> ExcluirSorteio(int idSorteio);
        Task<IEnumerable<NumeroEscolhido>> VisualizarNumerosPorIdPedido(int idPedido);
        Task<int> ConfirmarPagamentoRecebido(int idPedido, int idStatusPago);
        Task<IEnumerable<NumeroEscolhidoBody>> BuscarTodosNumerosSorteioPorId(int idSorteio);
        Task<IEnumerable<NumeroEscolhidoBody>> BuscarNumerosReservadoOuPagoSorteioPorId(int idSorteio, int idStatusPedido);
        Task<IEnumerable<Pedido>> ObterTodosPedidosPendentes(int statusPendente);
        Task<int> RemoverPedidoPendenteAposPrazoMaximo(Pedido pedido, int statusCancelado);
    }
}