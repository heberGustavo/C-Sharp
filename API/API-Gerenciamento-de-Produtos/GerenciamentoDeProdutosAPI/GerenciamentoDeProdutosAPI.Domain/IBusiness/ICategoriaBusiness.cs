using GerenciamentoDeProdutosAPI.Domain.IBusiness.Base;
using GerenciamentoDeProdutosAPI.Domain.Models.EntityDomain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GerenciamentoDeProdutosAPI.Domain.IBusiness
{
    public interface ICategoriaBusiness : IBusinessBase<Categoria>
    {
        Task<IEnumerable<Categoria>> FindAll();
        Task<Categoria> FindById(int id);
        Task<int> Create(Categoria categoria);
        Task<IEnumerable<Categoria>> FindByCategorySituation(string nomeCategoria, bool situacao);
        Task<Categoria> Update(Categoria categoria);
    }
}
