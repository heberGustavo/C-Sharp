using Teste.Domain.IBusiness.Base;
using Teste.Domain.Models.EntityDomain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Teste.Domain.IBusiness
{
    public interface IFuncionarioBusiness : IFuncionarioBase<Funcionario>
    {
        Task<IEnumerable<Funcionario>> ObterTodosFuncionarios();
        Task<ResultResponseModel> Cadastrar(Funcionario model);
        Task<Funcionario> ObterFuncionarioPorId(int id);
        Task<ResultResponseModel> Editar(Funcionario model);
        Task<int> VerificaSeExisteCadastrado(string nome, decimal salario);
        Task<ResultResponseModel> Excluir(int id);
    }
}
