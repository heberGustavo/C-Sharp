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
                else
                    result = await Update(model);
            }
            catch (Exception e)
            {
                result.Type = ResultType.ValidationError;
                result.Messages.Add("Erro ao cadastrar Função. Entre em contato com o Administrador.");
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

                var funcaoSelecionada = await _funcaoFuncionarioRepository.GetById(id);

                var modelResult = await _funcaoFuncionarioRepository.DeleteAsync(funcaoSelecionada);
                if (modelResult == null)
                {
                    result.Type = ResultType.ValidationError;
                    result.Messages.Add("Erro ao deletar Função. Tente novamente.");
                    return result;
                }

                result.Type = ResultType.CompleteExecution;
                result.Messages.Add(modelResult.FnfNome + " deletado com sucesso!");
                
            }
            catch (Exception)
            {
                result.Type = ResultType.ValidationError;
                result.Messages.Add("Erro ao deletar Função. Entre em contato com o Administrador.");
                return result;
            }

            return result;
        }

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
                if (idResult <= 0)
                {
                    result.Type = ResultType.ValidationError;
                    result.Messages.Add("Erro ao cadastrar Função. Tente novamente!");
                    return result;
                }

                result.Type = ResultType.CompleteExecution;
                result.Messages.Add("Função cadastrada com sucesso!");

            }
            catch (Exception e)
            {
                result.Type = ResultType.ValidationError;
                result.Messages.Add("Erro ao cadastrar Função. Entre em contato com o Administrador.");
                return result;
            }

            return result;

        }

        public async Task<ResultInfo> Update(FuncaoFuncionarioDTO funcaoFuncionario)
        {
            var result = new ResultInfo();

            try
            {
                var funcaoSameName = await _funcaoFuncionarioRepository.GetAllAsync(item => item.FnfNome == funcaoFuncionario.FnfNome && item.FnfId != funcaoFuncionario.FnfId);

                if (funcaoSameName.Count > 0)
                {
                    result.Messages.Add("Função já cadastrada!");
                    result.Type = ResultType.ValidationError;
                    return result;
                }

                var modelFuncao = await _funcaoFuncionarioRepository.UpdateAsync(funcaoFuncionario);
                if (modelFuncao == null)
                {
                    result.Type = ResultType.ValidationError;
                    result.Messages.Add("Erro ao alterar Função. Tente novamente!");
                    return result;
                }

                result.Type = ResultType.CompleteExecution;
                result.Messages.Add("Função alterada com sucesso!");

            }
            catch (Exception e)
            {
                result.Type = ResultType.ValidationError;
                result.Messages.Add("Erro ao cadastrar Função. Entre em contato com o Administrador.");
                return result;
            }

            return result;

        }

        #endregion

        #endregion

        #region Read

        public async Task<ResultInfo<FuncaoFuncionarioDTO>> GetAllAsync()
        {
            var result = new ResultInfo<FuncaoFuncionarioDTO>();

            try
            {
                result.Items = await _funcaoFuncionarioRepository.GetAllAsync(x => x.FnfNome);
            }
            catch (Exception ex)
            {
                result.Type = ResultType.ValidationError;
                result.Messages.Add("Erro ao listar dados. Entre em contato com o Administrador.");
                return result;
            }

            return result;
        }

        public async Task<IEnumerable<FuncaoFuncionarioDTO>> ObterCadastradosAtivos()
           => await _funcaoFuncionarioRepository.GetAllAsync();

        #endregion

       
    }
}
