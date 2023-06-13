using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GerenciamentoDeProdutosAPI.Domain.IRepository.Base
{
    public interface IRepositoryBase<TDomain> where TDomain : class
    {
        Task<IEnumerable<TDomain>> GetAllAsync();
        Task<List<TDomain>> GetAllAsync(Func<TDomain, bool> whereExpression);
        Task<TDomain> GetById(int? id);

        Task<int> CreateAsync(TDomain model);

        Task<TDomain> UpdateAsync(TDomain model);
    }
}
