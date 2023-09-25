using AdministrativoImperial.Domain.IRepository.Base;
using AutoMapper;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AdministrativoImperial.Data.Repository
{
    public class RepositoryBase<TDomain, TData> : IRepositoryBase<TDomain>, IDisposable
           where TDomain : class
           where TData : class
    {
        protected readonly SqlDataContext _dataContext;
        protected readonly IMapper _mapper;

        public RepositoryBase(SqlDataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<TDomain> GetById(int? id)
        {
            try
            {
                var data = await _dataContext.Connection.GetAsync<TData>(id);

                return _mapper.Map<TDomain>(data);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        public async Task<int> CreateAsync(TDomain model)
        {
            try
            {
                return await _dataContext.Connection.InsertAsync<TData>(_mapper.Map<TData>(model));
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        public async Task<TDomain> UpdateAsync(TDomain model)
        {
            try
            {
                var updated = await _dataContext.Connection.UpdateAsync<TData>(_mapper.Map<TData>(model));

                if (!updated)
                    throw new Exception("Erro ao realizar alteração");

                return model;
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        public async Task<TDomain> DeleteAsync(TDomain model)
        {
            try
            {
                var delete = await _dataContext.Connection.DeleteAsync<TData>(_mapper.Map<TData>(model));

                if (!delete)
                    throw new Exception("Erro ao realizar exclusão");

                return model;
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        public async Task<IEnumerable<TDomain>> GetAllAsync()
        {
            try
            {
                var resultData = await _dataContext.Connection.GetAllAsync<TData>();

                var result = _mapper.Map<IEnumerable<TDomain>>(resultData);

                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        public async Task<List<TDomain>> GetAllAsync(Func<TDomain, bool> whereExpression)
        {
            try
            {
                var resultData = await _dataContext.Connection.GetAllAsync<TData>();

                var result = _mapper.Map<List<TDomain>>(resultData.AsList());

                return result.Where(whereExpression).ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        public async Task<List<TDomain>> GetAllAsync(Func<TDomain, string> orderByExpression)
        {
            try
            {
                var resultData = await _dataContext.Connection.GetAllAsync<TData>();

                var result = _mapper.Map<List<TDomain>>(resultData.AsList());

                return result.OrderBy(orderByExpression).ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        public async Task<List<TDomain>> GetAllAsync(Func<TDomain, string> orderByExpression1, Func<TDomain, bool> orderByExpression2)
        {
            try
            {
                var resultData = await _dataContext.Connection.GetAllAsync<TData>();

                var result = _mapper.Map<List<TDomain>>(resultData.AsList());

                return result.OrderBy(orderByExpression1)
                             .OrderBy(orderByExpression2)
                             .ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        public async Task<List<TDomain>> GetAllAsync(Func<TDomain, bool> whereExpression, Func<TDomain, string> orderByExpression)
        {
            try
            {
                var resultData = await _dataContext.Connection.GetAllAsync<TData>();

                var result = _mapper.Map<List<TDomain>>(resultData.AsList());

                return result.Where(whereExpression)
                              .OrderBy(orderByExpression)
                              .ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        public async Task<List<TDomain>> GetAllAsync(Func<TDomain, bool> whereExpression, Func<TDomain, int> orderByExpression)
        {
            try
            {
                var resultData = await _dataContext.Connection.GetAllAsync<TData>();

                var result = _mapper.Map<List<TDomain>>(resultData.AsList());

                return result.Where(whereExpression)
                              .OrderBy(orderByExpression)
                              .ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

    }
}
