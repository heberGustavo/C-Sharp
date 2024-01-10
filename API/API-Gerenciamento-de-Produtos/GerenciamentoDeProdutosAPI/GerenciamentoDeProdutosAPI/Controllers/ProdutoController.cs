using GerenciamentoDeProdutosAPI.Domain.IBusiness;
using GerenciamentoDeProdutosAPI.Domain.Models.Common;
using GerenciamentoDeProdutosAPI.Domain.Models.EntityDomain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GerenciamentoDeProdutosAPI.UI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoBusiness _produtoBusiness;

        public ProdutoController(IProdutoBusiness produtoBusiness)
        {
            _produtoBusiness = produtoBusiness;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var resultItems = await _produtoBusiness.FindAll();
                var resultSuccess = new ResultResponseModel<IEnumerable<ProdutoDTO>>(false, "Sucesso ao buscar produtos", resultItems);

                return Ok(resultSuccess);
            }
            catch (Exception)
            {
                var resultError = new ResultResponseModel<ProdutoDTO>(true, "Erro ao buscar produtos", null);
                return NotFound(resultError);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var resultModel = await _produtoBusiness.FindById(id);

                if (resultModel == null)
                {
                    var resultError = new ResultResponseModel<ProdutoDTO>(true, string.Format("Produto com ID: {0} não encontrado", id), null);
                    return NotFound(resultError);
                }

                var resultSuccess = new ResultResponseModel<ProdutoDTO>(false, string.Format("Produto com ID: {0} encontrado", id), resultModel.ProId, resultModel);
                return Ok(resultSuccess);
            }
            catch (Exception e)
            {
                var resultError = new ResultResponseModel<ProdutoDTO>(true, "Erro ao busca produto", null);
                return BadRequest(resultError);
            }
        }

        [HttpGet("{idCategoria}/{descricaoProduto}/{situacao}")]
        public async Task<IActionResult> Get(int idCategoria, string descricaoProduto, bool situacao)
        {
            try
            {
                var resultModel = await _produtoBusiness.FilterByCategoryDescriptionSituation(idCategoria, descricaoProduto, situacao);

                if (resultModel == null)
                {
                    var resultError = new ResultResponseModel<ProdutoDTO>(true, string.Format("Não foram encontradas produtos com nome: {0} e situação: {1}", descricaoProduto, situacao), null);
                    return NotFound(resultError);
                }

                var resultSuccess = new ResultResponseModel<IEnumerable<ProdutoDTO>>(false, string.Format("Sucesso ao buscar produtos"), resultModel);
                return Ok(resultSuccess);
            }
            catch (Exception)
            {
                var resultError = new ResultResponseModel<ProdutoDTO>(true, "Erro ao buscar produtos", null);
                return BadRequest(resultError);
            }
           
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProdutoDTO produto)
        {
            try
            {
                if (produto == null) return BadRequest();

                if (produto.ProId <= 0)
                {
                    #region Create

                    var result = await _produtoBusiness.Create(produto);
                    var selecionarProdutoCadastrado = await _produtoBusiness.FindById(result);

                    var resultSuccess = new ResultResponseModel<ProdutoDTO>(false, string.Format("Sucesso ao cadastrar produto"), result ,selecionarProdutoCadastrado);
                    return Ok(resultSuccess);

                    #endregion
                }
                else
                {
                    #region Update

                    var result = await _produtoBusiness.Update(produto);
                    var selecionarProdutoCadastrado = await _produtoBusiness.FindById(produto.ProId);

                    var resultSuccess = new ResultResponseModel<ProdutoDTO>(false, string.Format("Sucesso ao atualizar produto"), selecionarProdutoCadastrado.ProId, selecionarProdutoCadastrado);
                    return Ok(resultSuccess);

                    #endregion
                }

            }
            catch (Exception)
            {
                var resultError = new ResultResponseModel<ProdutoDTO>(true, "Erro ao cadatrar produto", null);
                return BadRequest(resultError);
            }
        }

    }
}
