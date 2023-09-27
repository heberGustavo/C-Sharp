using AdministrativoImperial.Domain.Business.Base;
using AdministrativoImperial.Domain.IBusiness;
using AdministrativoImperial.Domain.IRepository;
using AdministrativoImperial.Domain.Models.Common;
using AdministrativoImperial.Domain.Models.EntityDomain;
using Gpnet.Common.ExecutionManager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AdministrativoImperial.Domain.Business
{
    public class FuncionarioBusiness : BusinessBase<FuncionarioDTO>, IFuncionarioBusiness
    {
        private readonly IFuncionarioRepository _funcionarioRepository;

        public FuncionarioBusiness(IFuncionarioRepository funcionarioRepository) : base(funcionarioRepository)
        {
            _funcionarioRepository = funcionarioRepository;
        }

        #region Write

        public async Task<ResultInfo> Cadastrar(FuncionarioDTO model)
        {
            var result = new ResultInfo();

            try
            {
                if (model.FunId <= 0)
                    result = await Inserir(model);
                else
                    result = await Alterar(model);

            }
            catch (Exception e)
            {
                result.Type = ResultType.ValidationError;
                result.Messages.Add("Erro ao cadastrar Funcionário. Tente novamente!");
            }

            return result;
        }

        public async Task<ResultInfo> Desativar(int funId)
        {
            var result = new ResultInfo();

            try
            {
                var funcionarioDesativar = await _funcionarioRepository.GetById(funId);
                if(funcionarioDesativar == null)
                {
                    result.Type = ResultType.ValidationError;
                    result.Messages.Add("Erro ao selecionar Funcionário");
                }

                funcionarioDesativar.FunStatus = true;

                var funcionarioAtualizado = await _funcionarioRepository.UpdateAsync(funcionarioDesativar);
                if(funcionarioAtualizado != null)
                    result.Messages.Add("Funcionário desativado com sucesso!");
                else
                    result.Messages.Add("Erro ao desativar Funcionário");
            }
            catch (Exception e)
            {
                result.Type = ResultType.ValidationError;
                result.Messages.Add("Erro ao cadastrar Funcionário. Tente novamente!");
            }

            return result;
        }

        #region Métodos privados

        private async Task<ResultInfo> Inserir(FuncionarioDTO model)
        {
            var result = new ResultInfo();

            try
            {
                var idCadastrado = await _funcionarioRepository.CreateAsync(model);
                if (idCadastrado > 0)
                    result.Messages.Add("Funcionário cadastrado com sucesso!");
                else
                {
                    result.Type = ResultType.ValidationError;
                    result.Messages.Add("Erro ao cadastrar Funcionário");
                }

            }
            catch (Exception e)
            {
                result.Type = ResultType.ValidationError;
                result.Messages.Add("Erro ao cadastrar Funcionário. Tente novamente.");
            }

            return result;
        }

        private async Task<ResultInfo> Alterar(FuncionarioDTO model)
        {
            var result = new ResultInfo();

            try
            {
                var modelAtualizada = await _funcionarioRepository.UpdateAsync(model);

                if (modelAtualizada != null)
                    result.Messages.Add("Funcionário atualizado com sucesso!");
                else
                {
                    result.Type = ResultType.ValidationError;
                    result.Messages.Add("Erro ao atualizar Funcionário");
                }

            }
            catch (Exception e)
            {
                result.Type = ResultType.ValidationError;
                result.Messages.Add("Erro ao cadastrar Funcionário. Tente novamente.");
            }

            return result;
        }

        #endregion

        #endregion

        #region Read

        public async Task<ResultInfo<FuncionarioDTO>> ObterCadastrados()
        {
            var result = new ResultInfo<FuncionarioDTO>();

            try
            {
                result.Items = await _funcionarioRepository.ObterCadastrados();
            }
            catch (Exception e)
            {
                result.Type = ResultType.ValidationError;
                result.Messages.Add("Erro ao obter dados dos Funcionários");
            }

            return result;
        }

        public async Task<ResultInfo<FuncionarioDTO>> Selecionar(int funId)
        {
            var result = new ResultInfo<FuncionarioDTO>();

            try
            {
                result.Item = await _funcionarioRepository.GetById(funId);
            }
            catch (Exception e)
            {
                result.Type = ResultType.ValidationError;
                result.Messages.Add("Erro ao selecionar funcionário");
            }

            return result;
        }

        #endregion

    }
}
