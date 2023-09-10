using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaoPro.Domain.IBusiness
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
