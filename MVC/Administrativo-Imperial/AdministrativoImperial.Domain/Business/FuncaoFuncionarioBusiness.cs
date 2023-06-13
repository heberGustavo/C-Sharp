using AdministrativoImperial.Domain.Business.Base;
using AdministrativoImperial.Domain.IBusiness;
using AdministrativoImperial.Domain.IRepository;
using AdministrativoImperial.Domain.IRepository.Base;
using AdministrativoImperial.Domain.Models.Common;
using AdministrativoImperial.Domain.Models.EntityDomain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AdministrativoImperial.Domain.Business
{
    public class FuncaoFuncionarioBusiness : BusinessBase<FuncaoFuncionario>, IFuncaoFuncionarioBusiness
    {
        private readonly IFuncaoFuncionarioRepository _funcaoFuncionarioRepository;

        public FuncaoFuncionarioBusiness(IFuncaoFuncionarioRepository funcaoFuncionarioRepository) : base(funcaoFuncionarioRepository)
        {
            _funcaoFuncionarioRepository = funcaoFuncionarioRepository;
        }

        #region Write

        public async Task<ResultResponseModel> Cadastrar(FuncaoFuncionario funcaoFuncionario)
        {
            try
            {
                var retorno = new ResultResponseModel();

                if (funcaoFuncionario.id_funcao_funcionario <= 0)
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
                funcaoSelecionada.excluido = true;

                var result = await _funcaoFuncionarioRepository.UpdateAsync(funcaoSelecionada);
                if (result != null)
                    return new ResultResponseModel(false, result.nome + " excluido com sucesso!");
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

        public async Task<IEnumerable<FuncaoFuncionario>> ObterCadastrados()
            => await _funcaoFuncionarioRepository.GetAllAsync(x => x.nome, y => y.excluido == true);

        public async Task<IEnumerable<FuncaoFuncionario>> ObterCadastradosAtivos()
           => await _funcaoFuncionarioRepository.GetAllAsync(x => x.excluido == false);

        #endregion

        #region Metodos Privados

        public async Task<ResultResponseModel> Inserir(FuncaoFuncionario funcaoFuncionario)
        {
            var funcionariosMesmoNome = await _funcaoFuncionarioRepository.GetAllAsync(nome => nome.nome == funcaoFuncionario.nome);

            if (funcionariosMesmoNome.Count > 0)
                return new ResultResponseModel(true, "Função já cadastrada.");

            var idCadastrado = await _funcaoFuncionarioRepository.CreateAsync(funcaoFuncionario);

            if (idCadastrado > 0)
                return new ResultResponseModel(false, "Função cadastrada com sucesso!");
            else
                return new ResultResponseModel(true, "Erro ao cadastrar Função. Tente novamente!");
        }

        public async Task<ResultResponseModel> Alterar(FuncaoFuncionario funcaoFuncionario)
        {
            var funcionariosMesmoNome = await _funcaoFuncionarioRepository.GetAllAsync(item => item.nome == funcaoFuncionario.nome && item.id_funcao_funcionario != funcaoFuncionario.id_funcao_funcionario);

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
