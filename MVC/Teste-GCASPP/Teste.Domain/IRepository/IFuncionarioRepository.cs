using Teste.Domain.IRepository.Base;
using Teste.Domain.Models.EntityDomain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Teste.Domain.IRepository
{
    public interface IFuncionarioRepository : IRepositoryBase<Funcionario>
    {
        Task<IEnumerable<Funcionario>> ObterTodosFuncionarios();
        Task<int> VerificaSeExisteCadastrado(string nome, decimal salario);
        Task<ResultResponseModel> Excluir(int id);
    }
}
