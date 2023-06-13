using GerenciamentoDeProdutosAPI.Domain.IBusiness.Base;
using GerenciamentoDeProdutosAPI.Domain.Models.EntityDomain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GerenciamentoDeProdutosAPI.Domain.IBusiness
{
    public interface IProdutoBusiness : IBusinessBase<Produto>
    {
        Task<IEnumerable<Produto>> FindAll();
        Task<Produto> FindById(int id);
        Task<int> Create(Produto produto);
        Task<Produto> Update(Produto produto);
        Task<IEnumerable<Produto>> FilterByCategoryDescriptionSituation(int idCategoria, string descricaoProduto, bool situacao);
    }
}
