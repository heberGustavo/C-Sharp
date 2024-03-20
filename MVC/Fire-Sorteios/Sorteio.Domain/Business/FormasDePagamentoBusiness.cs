using Sorteio.Domain.Business.Base;
using Sorteio.Domain.IBusiness;
using Sorteio.Domain.IRepository;
using Sorteio.Domain.IRepository.Base;
using Sorteio.Domain.Models.Body;
using Sorteio.Domain.Models.Common;
using Sorteio.Domain.Models.EntityDomain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sorteio.Domain.Business
{
    public class FormasDePagamentoBusiness : BusinessBase<FormasDePagamento>, IFormasDePagamentoBusiness
    {
        private readonly IFormasDePagamentoRepository _formasDePagamentoRepository;

        public FormasDePagamentoBusiness(IFormasDePagamentoRepository formasDePagamentoRepository) : base(formasDePagamentoRepository)
        {
            _formasDePagamentoRepository = formasDePagamentoRepository;
        }

        public async Task<ResultResponseModel> CriarNovaFormaDePagamento(FormasDePagamento formasDePagamento)
        {
            try
            {
                var idFormaDePagamento = await _formasDePagamentoRepository.CreateAsync(formasDePagamento);
                
                if(idFormaDePagamento == 0) return new ResultResponseModel(true, "Erro ao cadastrar Forma de Pagamento. Tente novamente!");
                
                return new ResultResponseModel(false, "Sucesso ao cadastra Forma de Pagamento!");
            }
            catch(Exception e)
            {
                return new ResultResponseModel(true, "Erro ao cadastrar Forma de Pagamento. Tente novamente!");
            }
        }

        public async Task<ResultResponseModel> EditarFormaDePagamento(FormasDePagamento body)
        {
            try
            {
                await _formasDePagamentoRepository.UpdateAsync(body);
                return new ResultResponseModel(false, "Sucesso ao atualizar Forma de Pagamento!");
            }
            catch(Exception e)
            {
                return new ResultResponseModel(true, "Erro ao atualizar Forma de Pagamento. Tente novamente!");
            }
        }

        public Task<int> ExcluirFormaDePagamento(int idFormaDePagamento)
            => _formasDePagamentoRepository.ExcluirFormaDePagamento(idFormaDePagamento);

        public async Task<FormasDePagamento> ObterFormaDePagamentoPorId(int idFormaDePagamento)
            => await _formasDePagamentoRepository.GetById(idFormaDePagamento);

        public async Task<IEnumerable<FormaDePagamentoBody>> ObterTodasFormasDePagamentoAtivo()
            => await _formasDePagamentoRepository.ObterTodasFormasDePagamentoAtivo();
    }
}
