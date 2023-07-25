using Teste.Domain.IRepository.Base;
using Teste.Domain.Models.EntityDomain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Teste.Domain.Models.Body;

namespace Teste.Domain.IRepository
{
    public interface IFilhoRepository : IRepositoryBase<Filho>
    {
        Task<IEnumerable<FilhoBody>> ObterTodosFilhos();
        Task<ResultResponseModel> Excluir(int id);
        Task<int> VerificaSeExisteFilhoCadastrado(int id);
    }
}
