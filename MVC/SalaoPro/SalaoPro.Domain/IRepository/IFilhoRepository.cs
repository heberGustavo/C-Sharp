using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaoPro.Domain.IRepository
{
    public interface IFilhoRepository : IRepositoryBase<Filho>
    {
        Task<IEnumerable<FilhoBody>> ObterTodosFilhos();
        Task<ResultResponseModel> Excluir(int id);
        Task<int> VerificaSeExisteFilhoCadastrado(int id);
    }
}
