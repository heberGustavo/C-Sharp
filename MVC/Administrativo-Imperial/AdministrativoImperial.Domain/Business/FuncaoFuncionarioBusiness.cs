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
    public class FuncaoFuncionarioBusiness : BusinessBase<FuncaoFuncionarioDTO>, IFuncaoFuncionarioBusiness
    {
        private readonly IFuncaoFuncionarioRepository _funcaoFuncionarioRepository;

        public FuncaoFuncionarioBusiness(IFuncaoFuncionarioRepository funcaoFuncionarioRepository) : base(funcaoFuncionarioRepository)
        {
            _funcaoFuncionarioRepository = funcaoFuncionarioRepository;
        }

        #region Write

        public async Task<ResultInfo> Create(FuncaoFuncionarioDTO model)
        {
            var result = new ResultInfo();

            try
            {

                if (model.FnfId <= 0)
                    result = await Insert(model);
                //else
                //    result = await Update(model);

                return result;

            }
            catch (Exception e)
            {
                result.Type = ResultType.ValidationError;
                result.Messages.Add("Erro ao cadastrar Função. Entre em contato com o Administrador.");
                return result;
            }
        }

        public async Task<ResultResponseModel> Deletar(int id)
        {
            try
            {
                if (id <= 0)
                    return new ResultResponseModel(true, "Erro ao selecionar identificador. Tente novamente!");

                var funcaoSelecionada = await _funcaoFuncionarioRepository.GetById(id);
                funcaoSelecionada.FnfStatus = true;

                var result = await _funcaoFuncionarioRepository.UpdateAsync(funcaoSelecionada);
                if (result != null)
                    return new ResultResponseModel(false, result.FnfNome + " excluido com sucesso!");
                else
                    return new ResultResponseModel(true, "Erro ao deletar Função. Tente novamente.");
            }
            catch (Exception)
            {
                return new ResultResponseModel(true, "Erro ao deletar Função. Entre em contato com o Administrador.");
            }
        }

        #endregion

        #region Read

        public async Task<ResultInfo<FuncaoFuncionarioDTO>> GetAllAsync()
        {
            var result = new ResultInfo<FuncaoFuncionarioDTO>();

            try
            {
                result.Items = await _funcaoFuncionarioRepository.GetAllAsync(x => x.FnfNome, y => y.FnfStatus == true);
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }

        public async Task<IEnumerable<FuncaoFuncionarioDTO>> ObterCadastradosAtivos()
           => await _funcaoFuncionarioRepository.GetAllAsync(x => x.FnfStatus == false);

        #endregion

        #region Metodos Privados

        public async Task<ResultInfo> Insert(FuncaoFuncionarioDTO funcaoFuncionario)
        {
            var result = new ResultInfo();

            try
            {
                var funcaoSameName = await _funcaoFuncionarioRepository.GetAllAsync(nome => nome.FnfNome == funcaoFuncionario.FnfNome);

                if (funcaoSameName.Count > 0)
                {
                    result.Messages.Add("Função já cadastrada!");
                    result.Type = ResultType.ValidationError;
                    return result;
                }

                var idResult = await _funcaoFuncionarioRepository.CreateAsync(funcaoFuncionario);

                if (idResult > 0)
                {
                    result.Type = ResultType.CompleteExecution;
                    result.Messages.Add("Função cadastrada com sucesso!");
                    return result;
                }
                else
                {
                    result.Type = ResultType.ValidationError;
                    result.Messages.Add("Erro ao cadastrar Função. Tente novamente!");
                    return result;
                }
                
            }
            catch (Exception e)
            {
                result.Type = ResultType.ValidationError;
                result.Messages.Add("Erro ao cadastrar Função. Entre em contato com o Administrador.");
                return result;
            }
            
        }

        public async Task<ResultResponseModel> Update(FuncaoFuncionarioDTO funcaoFuncionario)
        {
            var funcaoSameName = await _funcaoFuncionarioRepository.GetAllAsync(item => item.FnfNome == funcaoFuncionario.FnfNome && item.FnfId != funcaoFuncionario.FnfId);

            if (funcaoSameName.Count > 0)
                return new ResultResponseModel(true, "Função já cadastrada.");

            var modelFuncao = await _funcaoFuncionarioRepository.UpdateAsync(funcaoFuncionario);

            if (modelFuncao != null)
                return new ResultResponseModel(false, "Função cadastrada com sucesso!");
            else
                return new ResultResponseModel(true, "Erro ao cadastrar Função. Tente novamente!");
        }

        #endregion
    }
}
