using Sorteio.Domain.IBusiness.Base;
using Sorteio.Domain.Models.Body;
using Sorteio.Domain.Models.Common;
using Sorteio.Domain.Models.EntityDomain;
using Sorteio.Domain.Models.NotMapped;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sorteio.Domain.IBusiness
{
    public interface ISorteiosBusiness : IBusinessBase<Models.EntityDomain.Sorteio>
    {
        Task<ResultResponseModel> CriarNovoSorteio(SorteioBody sorteioBody);
        Task<IEnumerable<SorteioNotMapped>> ObterTodosSorteio();
        Task<ResultResponseModel> FinalizarSorteio(VencedorSorteio vencedorSorteio);
        Task<IEnumerable<InformacoesSorteio>> ObterInformacoesSorteio();
        Task<IEnumerable<Models.EntityDomain.Sorteio>> ObterTodosSorteioAtivos();
        Task<SorteioNotMapped> ObterSorteioPorId(int idSorteio);
        Task<IEnumerable<MeusBilhetes>> ObterSorteiosBilheteClientePorId(int id_usuario);
        Task<bool> EditarFinalizarSorteio(VencedorSorteio body);
        Task<IEnumerable<MeusPremios>> ObterMeusPremiosClientePorId(int id_usuario);
        Task<ResultResponseModel> EditarSorteio(SorteioBody body);
        Task<IEnumerable<Pedido>> ObterTodosPedidosPendentes();
        Task<IEnumerable<InformacoesSorteio>> FiltrarSorteioPorCategoria(int idCategoria);
        Task<IEnumerable<InformacoesSorteio>> ObterTodosUltimosSorteiosRealizados();
        Task<SorteioBody> ObterDadosDoSorteioPorId(int idSorteio);
        Task<IEnumerable<NumeroEscolhidoBody>> ObterNumerosDoSorteioPorId(int idSorteio);
        Task<IEnumerable<ParticipanteSorteio>> ObterParticipantesSorteioPorId(int idSorteio);
        Task<int> RemoverPedidoPendenteAposPrazoMaximo(Pedido pedido);
        Task<int> ExcluirSorteio(int idSorteio);
        Task<IEnumerable<NumeroEscolhido>> VisualizarNumerosPorIdPedido(int idPedido);
        Task<int> ConfirmarPagamentoRecebido(int idPedido);
        Task<IEnumerable<NumeroEscolhidoBody>> BuscarTodosNumerosSorteioPorId(int idSorteio);
        Task<IEnumerable<NumeroEscolhidoBody>> BuscarNumerosReservadoOuPagoSorteioPorId(int idSorteio, int idStatusPedido);
    }
}