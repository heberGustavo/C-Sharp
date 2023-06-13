using AdministrativoImperial.Domain.IBusiness.Base;
using AdministrativoImperial.Domain.IRepository.Base;
using AdministrativoImperial.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AdministrativoImperial.Domain.Business.Base
{
    public class BusinessBase<TDomain> : IBusinessBase<TDomain>
        where TDomain : class
    {
        protected IRepositoryBase<TDomain> _defaultRepository;

        public BusinessBase(IRepositoryBase<TDomain> repository)
        {
            _defaultRepository = repository;
        }

        #region GET
        public async Task<TDomain> GetById(int id)
        {
            try
            {
                return await _defaultRepository.GetById(id);
            }
            catch (System.Exception)
            {
                return null;
            }
        }
        #endregion

        #region CREATE
        public async Task<ResultResponseModel> CreateAsync(TDomain model)
        {
            try
            {
                var resultado = await _defaultRepository.CreateAsync(model);

                if (resultado == 0) return new ResultResponseModel(true, "Erro ao criar");

                return new ResultResponseModel(false, "Sucesso ao criar");

            }
            catch (Exception ex)
            {
                return new ResultResponseModel(true, ex.Message);
            }
        }
        #endregion

        #region UPDATE
        public async Task<ResultResponseModel> UpdateAsync(TDomain model)
        {
            try
            {
                var resultado = await _defaultRepository.UpdateAsync(model);

                if (resultado == null) return new ResultResponseModel(true, "Erro ao alterar");

                return new ResultResponseModel(false, "Sucesso ao alterar");
            }
            catch (Exception ex)
            {
                return new ResultResponseModel(true, ex.Message);
            }
        }
        #endregion      
    }
}
