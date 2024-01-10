using GerenciamentoDeProdutosAPI.Domain.IBusiness.Base;
using GerenciamentoDeProdutosAPI.Domain.Models.EntityDomain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GerenciamentoDeProdutosAPI.Domain.IBusiness
{
    public interface ICategoriaBusiness : IBusinessBase<CategoriaDTO>
    {
        Task<IEnumerable<CategoriaDTO>> FindAll();
        Task<CategoriaDTO> FindById(int id);
        Task<int> Create(CategoriaDTO categoria);
        Task<IEnumerable<CategoriaDTO>> FindByCategorySituation(string nomeCategoria, bool situacao);
        Task<CategoriaDTO> Update(CategoriaDTO categoria);
    }
}
