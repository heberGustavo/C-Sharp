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
    public class ProdutoBusiness : BusinessBase<ProdutoDTO>, IProdutoBusiness
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoBusiness(IProdutoRepository produtoRepository) : base(produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        #region Read

        public Task<IEnumerable<ProdutoDTO>> FindAll()
            => _produtoRepository.GetAllAsync();

        public Task<ProdutoDTO> FindById(int id)
            => _produtoRepository.GetById(id);

        public Task<IEnumerable<ProdutoDTO>> FilterByCategoryDescriptionSituation(int idCategoria, string descricaoProduto, bool situacao)
            => _produtoRepository.FilterByCategoryDescriptionSituation(idCategoria, descricaoProduto, situacao);
        #endregion

        #region Write

        public Task<int> Create(ProdutoDTO produto)
        {
            var result = _produtoRepository.CreateAsync(produto);
            return result;
        }

        public Task<ProdutoDTO> Update(ProdutoDTO produto)
        {
            var result = _produtoRepository.UpdateAsync(produto);
            return result;
        }

        #endregion

    }
}
