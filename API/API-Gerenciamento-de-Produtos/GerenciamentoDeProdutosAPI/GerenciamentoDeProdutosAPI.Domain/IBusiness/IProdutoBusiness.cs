using GerenciamentoDeProdutosAPI.Domain.IBusiness.Base;
using GerenciamentoDeProdutosAPI.Domain.Models.EntityDomain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GerenciamentoDeProdutosAPI.Domain.IBusiness
{
    public interface IProdutoBusiness : IBusinessBase<ProdutoDTO>
    {
        Task<IEnumerable<ProdutoDTO>> FindAll();
        Task<ProdutoDTO> FindById(int id);
        Task<int> Create(ProdutoDTO produto);
        Task<ProdutoDTO> Update(ProdutoDTO produto);
        Task<IEnumerable<ProdutoDTO>> FilterByCategoryDescriptionSituation(int idCategoria, string descricaoProduto, bool situacao);
    }
}
