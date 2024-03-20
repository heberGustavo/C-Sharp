using Sorteio.Domain.Business.Base;
using Sorteio.Domain.IBusiness;
using Sorteio.Domain.IRepository;
using Sorteio.Domain.IRepository.Base;
using Sorteio.Domain.Models.Common;
using Sorteio.Domain.Models.EntityDomain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sorteio.Domain.Business
{
    public class TipoFormasDePagamnetoBusiness : BusinessBase<TipoFormaDePagamento>, ITipoFormasDePagamentoBusiness
    {
        private readonly ITipoFormasDePagamentoRepository _tipoFormasDePagamentoRepository;

        public TipoFormasDePagamnetoBusiness(ITipoFormasDePagamentoRepository tipoFormasDePagamentoRepository) : base(tipoFormasDePagamentoRepository)
        {
            _tipoFormasDePagamentoRepository = tipoFormasDePagamentoRepository;
        }

        public async Task<IEnumerable<TipoFormaDePagamento>> ObterTodasFormasDePagamentoAtiva()
            => await _tipoFormasDePagamentoRepository.GetAllAsync(t => t.status == false);
    }
}
