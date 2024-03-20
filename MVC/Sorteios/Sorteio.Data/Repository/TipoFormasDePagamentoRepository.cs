using AutoMapper;
using Dapper;
using Sorteio.Data.EntityData;
using Sorteio.Data.Repository.Base;
using Sorteio.Domain.IRepository;
using Sorteio.Domain.Models.Common;
using Sorteio.Domain.Models.EntityDomain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sorteio.Data.Repository
{
    public class TipoFormasDePagamentoRepository : RepositoryBase<TipoFormaDePagamento, TipoFormaDePagamentoData>, ITipoFormasDePagamentoRepository
    {
        public TipoFormasDePagamentoRepository(SqlDataContext dataContext, IMapper mapper) : base(dataContext, mapper)
        {
        }
    }
}
