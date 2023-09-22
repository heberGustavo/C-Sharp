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

        public async Task<ResultResponseModel> Cadastrar(FuncaoFuncionarioDTO funcaoFuncionario)
        {
            try
            {
                var retorno = new ResultResponseModel();

                if (funcaoFuncionario.FnfId <= 0)
                    retorno = await Inserir(funcaoFuncionario);
                else
                    retorno = await Alterar(funcaoFuncionario);

                return retorno;

            }
            catch (Exception e)
            {
                return new ResultResponseModel(true, "Erro ao cadastrar Função. Entre em contato com o Administrador.");
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

        public async Task<IEnumerable<FuncaoFuncionarioDTO>> ObterCadastrados()
        {
            return await _funcaoFuncionarioRepository.GetAllAsync(x => x.FnfNome, y => y.FnfStatus == true);
        }

        public async Task<IEnumerable<FuncaoFuncionarioDTO>> ObterCadastradosAtivos()
           => await _funcaoFuncionarioRepository.GetAllAsync(x => x.FnfStatus == false);

        #endregion

        #region Metodos Privados

        public async Task<ResultResponseModel> Inserir(FuncaoFuncionarioDTO funcaoFuncionario)
        {
            var funcionariosMesmoNome = await _funcaoFuncionarioRepository.GetAllAsync(nome => nome.FnfNome == funcaoFuncionario.FnfNome);

            if (funcionariosMesmoNome.Count > 0)
                return new ResultResponseModel(true, "Função já cadastrada.");

            var idCadastrado = await _funcaoFuncionarioRepository.CreateAsync(funcaoFuncionario);

            if (idCadastrado > 0)
                return new ResultResponseModel(false, "Função cadastrada com sucesso!");
            else
                return new ResultResponseModel(true, "Erro ao cadastrar Função. Tente novamente!");
        }

        public async Task<ResultResponseModel> Alterar(FuncaoFuncionarioDTO funcaoFuncionario)
        {
            var funcionariosMesmoNome = await _funcaoFuncionarioRepository.GetAllAsync(item => item.FnfNome == funcaoFuncionario.FnfNome && item.FnfId != funcaoFuncionario.FnfId);

            if (funcionariosMesmoNome.Count > 0)
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
