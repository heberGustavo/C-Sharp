using Teste.Domain.Business.Base;
using Teste.Domain.IBusiness;
using Teste.Domain.IRepository;
using Teste.Domain.Models.EntityDomain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Teste.Domain.Business
{
    public class FuncionarioBusiness : BusinessBase<Funcionario>, IFuncionarioBusiness
    {
        private readonly IFuncionarioRepository _funcionarioRepository;

        public FuncionarioBusiness(IFuncionarioRepository funcionarioRepository) : base(funcionarioRepository)
        {
            _funcionarioRepository = funcionarioRepository;
        }

        public async Task<ResultResponseModel> Cadastrar(Funcionario model)
        {
            try
            {
                var result = await _funcionarioRepository.CreateAsync(model);
                if (result == 0) return new ResultResponseModel(true, "Erro ao cadastrar Funcionário. Tente novamente!");

                return new ResultResponseModel(false, "Funcionário cadastrado com sucesso!");
            }
            catch(Exception e)
            {
                return new ResultResponseModel(true, "Erro ao cadastrar Funcionário. Contate o administrador.");
            }
        }

        public async Task<ResultResponseModel> Editar(Funcionario model)
        {
            try
            {
                var result = await _funcionarioRepository.UpdateAsync(model);
                if (result == null) return new ResultResponseModel(true, "Erro ao editar Funcionário. Tente novamente.");

                return new ResultResponseModel(false, "Funcionário alterado com sucesso!");
            }
            catch(Exception e)
            {
                return new ResultResponseModel(true, "Ocorreu um erro. Contate o administrador.");
            }
            
        }

        public Task<ResultResponseModel> Excluir(int id)
            => _funcionarioRepository.Excluir(id);

        public Task<Funcionario> ObterFuncionarioPorId(int id)
            => _funcionarioRepository.GetById(id);

        public Task<IEnumerable<Funcionario>> ObterTodosFuncionarios()
            => _funcionarioRepository.ObterTodosFuncionarios();

        public Task<int> VerificaSeExisteCadastrado(string nome, decimal salario)
            => _funcionarioRepository.VerificaSeExisteCadastrado(nome, salario);
    }
}
