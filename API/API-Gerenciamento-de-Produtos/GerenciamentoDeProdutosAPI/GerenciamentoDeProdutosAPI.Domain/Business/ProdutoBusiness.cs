using GerenciamentoDeProdutosAPI.Domain.Business.Base;
using GerenciamentoDeProdutosAPI.Domain.IBusiness;
using GerenciamentoDeProdutosAPI.Domain.IRepository;
using GerenciamentoDeProdutosAPI.Domain.Models.EntityDomain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GerenciamentoDeProdutosAPI.Domain.Business
{
    public class ProdutoBusiness : BusinessBase<Produto>, IProdutoBusiness
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoBusiness(IProdutoRepository produtoRepository) : base(produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        #region Read

        public Task<IEnumerable<Produto>> FindAll()
            => _produtoRepository.GetAllAsync();

        public Task<Produto> FindById(int id)
            => _produtoRepository.GetById(id);

        public Task<IEnumerable<Produto>> FilterByCategoryDescriptionSituation(int idCategoria, string descricaoProduto, bool situacao)
            => _produtoRepository.FilterByCategoryDescriptionSituation(idCategoria, descricaoProduto, situacao);
        #endregion

        #region Write

        public Task<int> Create(Produto produto)
        {
            var result = _produtoRepository.CreateAsync(produto);
            return result;
        }

        public Task<Produto> Update(Produto produto)
        {
            var result = _produtoRepository.UpdateAsync(produto);
            return result;
        }

        #endregion

    }
}
