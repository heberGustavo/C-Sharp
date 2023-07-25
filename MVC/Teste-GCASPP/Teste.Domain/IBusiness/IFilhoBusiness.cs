using Teste.Domain.IBusiness.Base;
using Teste.Domain.Models.EntityDomain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Teste.Domain.Models.Body;

namespace Teste.Domain.IBusiness
{
    public interface IFilhoBusiness : IFuncionarioBase<Filho>
    {
        Task<IEnumerable<FilhoBody>> ObterTodosFilhos();
        Task<ResultResponseModel> Cadastrar(Filho model);
        Task<Filho> ObterFilhoPorId(int id);
        Task<ResultResponseModel> Editar(Filho model);
        Task<ResultResponseModel> Excluir(int id);
        Task<int> VerificaSeExisteFilhoCadastrado(int id);
    }
}
