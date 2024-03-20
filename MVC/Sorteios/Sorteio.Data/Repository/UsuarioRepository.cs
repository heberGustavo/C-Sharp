using AutoMapper;
using Dapper;
using Sorteio.Common;
using Sorteio.Data.EntityData;
using Sorteio.Data.Repository.Base;
using Sorteio.Domain.IRepository;
using Sorteio.Domain.Models.Body;
using Sorteio.Domain.Models.Common;
using Sorteio.Domain.Models.EntityDomain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sorteio.Data.Repository
{
    public class UsuarioRepository : RepositoryBase<Usuario, UsuarioData>, IUsuarioRepository
    {
        public UsuarioRepository(SqlDataContext dataContext, IMapper mapper) : base(dataContext, mapper)
        {
        }

        public Task<IEnumerable<NumeroEscolhidoBody>> ConsultarNumerosCliente(string celularUsuario, int idSorteio)
            => _dataContext.Connection.QueryAsync<NumeroEscolhidoBody>(@"SELECT ne.id_numero_escolhido, p.id_pedido, ne.numero, p.id_status_pedido, u.nome 
                                                                         FROM Pedido p 
                                                                         LEFT JOIN Usuario u ON u.id_usuario = p.id_usuario 
                                                                         LEFT JOIN NumeroEscolhido ne ON ne.id_pedido = p.id_pedido 
                                                                         WHERE u.celular = @celularUsuario AND p.id_sorteio = @idSorteio", new { celularUsuario, idSorteio });

        public Task<IEnumerable<NumeroEscolhidoBody>> MostrarNumerosDoUsuario(int idUsuario)
            => _dataContext.Connection.QueryAsync<NumeroEscolhidoBody>($"SELECT ne.id_numero_escolhido, p.id_pedido, ne.numero, p.id_status_pedido " +
                                                                       $"FROM Pedido p " +
                                                                       $"LEFT JOIN NumeroEscolhido ne ON ne.id_pedido = p.id_pedido " +
                                                                       $"WHERE p.id_usuario = @idUsuario AND p.id_status_pedido = ${DataDictionary.STATUS_PEDIDO_PENDENTE}", new { idUsuario });
    }
}
