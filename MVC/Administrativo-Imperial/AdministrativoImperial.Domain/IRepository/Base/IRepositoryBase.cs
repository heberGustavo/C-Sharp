using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AdministrativoImperial.Domain.IRepository.Base
{
    public interface IRepositoryBase<TDomain> where TDomain : class
    {
        Task<IEnumerable<TDomain>> GetAllAsync();
        Task<List<TDomain>> GetAllAsync(Func<TDomain, bool> whereExpression);
        Task<List<TDomain>> GetAllAsync(Func<TDomain, bool> whereExpression, Func<TDomain, string> orderByExpression);
        Task<List<TDomain>> GetAllAsync(Func<TDomain, bool> whereExpression, Func<TDomain, int> orderByExpression);
        Task<List<TDomain>> GetAllAsync(Func<TDomain, string> orderByExpression);
        Task<List<TDomain>> GetAllAsync(Func<TDomain, string> orderByExpression1, Func<TDomain, bool> orderByExpression2);
        Task<TDomain> GetById(int? id);
        Task<int> CreateAsync(TDomain model);
        Task<TDomain> UpdateAsync(TDomain model);
        Task<TDomain> DeleteAsync(TDomain model);

    }
}
