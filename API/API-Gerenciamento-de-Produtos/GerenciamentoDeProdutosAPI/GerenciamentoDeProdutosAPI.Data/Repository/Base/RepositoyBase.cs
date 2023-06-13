using AutoMapper;
using Dapper;
using Dapper.Contrib.Extensions;
using GerenciamentoDeProdutosAPI.Domain.IRepository.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciamentoDeProdutosAPI.Data.Repository.Base
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
    }
}
