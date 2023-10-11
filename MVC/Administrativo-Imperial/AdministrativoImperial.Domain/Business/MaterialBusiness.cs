using AdministrativoImperial.Domain.Business.Base;
using AdministrativoImperial.Domain.IBusiness;
using AdministrativoImperial.Domain.IRepository;
using AdministrativoImperial.Domain.IRepository.Base;
using AdministrativoImperial.Domain.Models.Common;
using AdministrativoImperial.Domain.Models.EntityDomain;
using Gpnet.Common.ExecutionManager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AdministrativoImperial.Domain.Business
{
    public class MaterialBusiness : BusinessBase<MaterialDTO>, IMaterialBusiness
    {
        private readonly IMaterialRepository _materialRepository;

        public MaterialBusiness(IMaterialRepository materialRepository) : base(materialRepository)
        {
            _materialRepository = materialRepository;
        }

        #region Write

        public async Task<ResultInfo> Create(MaterialDTO model)
        {
            var result = new ResultInfo();

            try
            {
                if (model.MtrId <= 0)
                    result = await Insert(model);
                else
                    result = await Update(model);
            }
            catch (Exception e)
            {
                result.Type = ResultType.ValidationError;
                result.Messages.Add("Erro ao cadastrar Material. Entre em contato com o Administrador.");
                return result;
            }

            return result;
        }

        public async Task<ResultInfo> Deletar(int id)
        {
            var result = new ResultInfo();
            
            try
            {
                if (id <= 0)
                {
                    result.Type = ResultType.ValidationError;
                    result.Messages.Add("Erro ao selecionar identificador. Tente novamente!");
                    return result;
                }

                var materialSelecionado = await _materialRepository.GetById(id);

                var modelResult = await _materialRepository.DeleteAsync(materialSelecionado);
                if (modelResult == null)
                {
                    result.Type = ResultType.ValidationError;
                    result.Messages.Add("Erro ao deletar Material. Tente novamente.");
                    return result;
                }

                result.Type = ResultType.CompleteExecution;
                result.Messages.Add(modelResult.MtrNome + " deletado com sucesso!");
                
            }
            catch (Exception)
            {
                result.Type = ResultType.ValidationError;
                result.Messages.Add("Erro ao deletar Material. Entre em contato com o Administrador.");
                return result;
            }

            return result;
        }

        #region Metodos Privados

        public async Task<ResultInfo> Insert(MaterialDTO funcaoFuncionario)
        {
            var result = new ResultInfo();

            try
            {
                var idResult = await _materialRepository.CreateAsync(funcaoFuncionario);
                if (idResult <= 0)
                {
                    result.Type = ResultType.ValidationError;
                    result.Messages.Add("Erro ao cadastrar Material. Tente novamente!");
                    return result;
                }

                result.Type = ResultType.CompleteExecution;
                result.Messages.Add("Material cadastrado com sucesso!");

            }
            catch (Exception e)
            {
                result.Type = ResultType.ValidationError;
                result.Messages.Add("Erro ao cadastrar Material. Entre em contato com o Administrador.");
                return result;
            }

            return result;

        }

        public async Task<ResultInfo> Update(MaterialDTO funcaoFuncionario)
        {
            var result = new ResultInfo();

            try
            {
                var modelFuncao = await _materialRepository.UpdateAsync(funcaoFuncionario);
                if (modelFuncao == null)
                {
                    result.Type = ResultType.ValidationError;
                    result.Messages.Add("Erro ao alterar Material. Tente novamente!");
                    return result;
                }

                result.Type = ResultType.CompleteExecution;
                result.Messages.Add("Material alterado com sucesso!");

            }
            catch (Exception e)
            {
                result.Type = ResultType.ValidationError;
                result.Messages.Add("Erro ao cadastrar Material. Entre em contato com o Administrador.");
                return result;
            }

            return result;

        }

        #endregion

        #endregion

        #region Read

        public async Task<ResultInfo<MaterialDTO>> GetAllAsync()
        {
            var result = new ResultInfo<MaterialDTO>();

            try
            {
                result.Items = await _materialRepository.ObterCadastrados();
            }
            catch (Exception ex)
            {
                result.Type = ResultType.ValidationError;
                result.Messages.Add("Erro ao listar dados. Entre em contato com o Administrador.");
                return result;
            }

            return result;
        }

        public async Task<IEnumerable<MaterialDTO>> ObterCadastradosAtivos()
           => await _materialRepository.GetAllAsync(x => x.MtrDataCompra.ToString());

        public async Task<ResultInfo<MaterialDTO>> Selecionar(int funId)
        {
            var result = new ResultInfo<MaterialDTO>();

            try
            {
                result.Item = await _materialRepository.GetById(funId);
            }
            catch (Exception e)
            {
                result.Type = ResultType.ValidationError;
                result.Messages.Add("Erro ao selecionar material");
            }

            return result;
        }

        #endregion


    }
}
