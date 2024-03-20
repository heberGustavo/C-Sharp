using Sorteio.Common;
using Sorteio.Domain.Business.Base;
using Sorteio.Domain.IBusiness;
using Sorteio.Domain.IRepository;
using Sorteio.Domain.IRepository.Base;
using Sorteio.Domain.Models.Body;
using Sorteio.Domain.Models.Common;
using Sorteio.Domain.Models.EntityDomain;
using Sorteio.Domain.Models.NotMapped;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sorteio.Domain.Business
{
    public class SorteiosBusiness : BusinessBase<Models.EntityDomain.Sorteio>, ISorteiosBusiness
    {
        private readonly ISorteiosRepository _sorteiosRepository;

        public SorteiosBusiness(ISorteiosRepository sorteiosRepository) : base(sorteiosRepository)
        {
            _sorteiosRepository = sorteiosRepository;
        }

        public Task<ResultResponseModel> CriarNovoSorteio(SorteioBody sorteioBody)
            => _sorteiosRepository.CriarNovoSorteio(sorteioBody);

        public Task<bool> EditarFinalizarSorteio(VencedorSorteio body)
            => _sorteiosRepository.EditarFinalizarSorteio(body);

        public Task<ResultResponseModel> EditarSorteio(SorteioBody body)
            => _sorteiosRepository.EditarSorteio(body);

        public Task<IEnumerable<InformacoesSorteio>> FiltrarSorteioPorCategoria(int idCategoria)
            => _sorteiosRepository.FiltrarSorteioPorCategoria(idCategoria);

        public Task<ResultResponseModel> FinalizarSorteio(VencedorSorteio vencedorSorteio)
            => _sorteiosRepository.FinalizarSorteio(vencedorSorteio);

        public Task<SorteioBody> ObterDadosDoSorteioPorId(int idSorteio)
            => _sorteiosRepository.ObterDadosDoSorteioPorId(idSorteio);

        public Task<IEnumerable<InformacoesSorteio>> ObterInformacoesSorteio()
            => _sorteiosRepository.ObterInformacoesSorteio();

        public Task<IEnumerable<MeusPremios>> ObterMeusPremiosClientePorId(int id_usuario)
            => _sorteiosRepository.ObterMeusPremiosClientePorId(id_usuario);

        public Task<SorteioNotMapped> ObterSorteioPorId(int idSorteio)
            => _sorteiosRepository.ObterSorteioPorId(idSorteio);

        public Task<IEnumerable<MeusBilhetes>> ObterSorteiosBilheteClientePorId(int id_usuario)
            => _sorteiosRepository.ObterSorteiosBilheteClientePorId(id_usuario);

        public Task<IEnumerable<NumeroEscolhidoBody>> ObterNumerosDoSorteioPorId(int idSorteio)
            => _sorteiosRepository.ObterNumerosDoSorteioPorId(idSorteio);

        public Task<IEnumerable<SorteioNotMapped>> ObterTodosSorteio()
            => _sorteiosRepository.ObterTodosSorteio();

        public Task<IEnumerable<InformacoesSorteio>> ObterTodosUltimosSorteiosRealizados()
            => _sorteiosRepository.ObterTodosUltimosSorteiosRealizados();

        public async Task<IEnumerable<Models.EntityDomain.Sorteio>> ObterTodosSorteioAtivos()
            => await _sorteiosRepository.GetAllAsync(x => x.status == false);

        public Task<IEnumerable<ParticipanteSorteio>> ObterParticipantesSorteioPorId(int idSorteio)
            => _sorteiosRepository.ObterParticipantesSorteioPorId(idSorteio);

        public Task<int> ExcluirSorteio(int idSorteio)
            => _sorteiosRepository.ExcluirSorteio(idSorteio);

        public Task<IEnumerable<NumeroEscolhido>> VisualizarNumerosPorIdPedido(int idPedido)
            => _sorteiosRepository.VisualizarNumerosPorIdPedido(idPedido);

        public Task<int> ConfirmarPagamentoRecebido(int idPedido)
            => _sorteiosRepository.ConfirmarPagamentoRecebido(idPedido, DataDictionary.STATUS_PEDIDO_PAGO);

        public Task<IEnumerable<NumeroEscolhidoBody>> BuscarTodosNumerosSorteioPorId(int idSorteio)
            => _sorteiosRepository.BuscarTodosNumerosSorteioPorId(idSorteio);

        public Task<IEnumerable<NumeroEscolhidoBody>> BuscarNumerosReservadoOuPagoSorteioPorId(int idSorteio, int idStatusPedido)
            => _sorteiosRepository.BuscarNumerosReservadoOuPagoSorteioPorId(idSorteio, idStatusPedido);

        public Task<IEnumerable<Pedido>> ObterTodosPedidosPendentes()
            => _sorteiosRepository.ObterTodosPedidosPendentes(DataDictionary.STATUS_PEDIDO_PENDENTE);

        public Task<int> RemoverPedidoPendenteAposPrazoMaximo(Pedido pedido)
            => _sorteiosRepository.RemoverPedidoPendenteAposPrazoMaximo(pedido, DataDictionary.STATUS_PEDIDO_CANCELADO);
    }
}