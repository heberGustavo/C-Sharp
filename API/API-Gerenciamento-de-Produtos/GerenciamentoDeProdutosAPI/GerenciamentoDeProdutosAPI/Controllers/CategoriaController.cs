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
    [Route("[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaBusiness _categoriaBusiness;

        public CategoriaController(ICategoriaBusiness categoriaBusiness)
        {
            _categoriaBusiness = categoriaBusiness;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var resultItems = await _categoriaBusiness.FindAll();
                var resultSuccess = new ResultResponseModel<IEnumerable<CategoriaDTO>>(false, "Sucesso ao buscar categorias", resultItems);

                return Ok(resultSuccess);
            }
            catch (Exception e)
            {
                var resultError = new ResultResponseModel<CategoriaDTO>(true, "Erro ao buscar categorias", null);
                return NotFound(resultError);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var resultModel = await _categoriaBusiness.FindById(id);

                if (resultModel == null)
                {
                    var resultError = new ResultResponseModel<CategoriaDTO>(true, string.Format("Categoria com ID: {0} não encontrada", id), null);
                    return NotFound(resultError);
                }

                var resultSuccess = new ResultResponseModel<CategoriaDTO>(false, string.Format("Categoria com ID: {0} encontrada", id), resultModel.CatId, resultModel);
                return Ok(resultSuccess);
            }
            catch (Exception e)
            {
                var resultError = new ResultResponseModel<CategoriaDTO>(true, "Erro ao busca categoria", null);
                return BadRequest(resultError);
            }
        }

        [HttpGet("{nomeCategoria}/{situacao}")]
        public async Task<IActionResult> Get(string nomeCategoria, bool situacao)
        {
            try
            {
                var resultModel = await _categoriaBusiness.FindByCategorySituation(nomeCategoria, situacao);

                if (resultModel == null)
                {
                    var resultError = new ResultResponseModel<CategoriaDTO>(true, string.Format("Não foram encontradas categorias com nome: {0} e situação: {1}", nomeCategoria, situacao), null);
                    return NotFound(resultError);
                }

                var resultSuccess = new ResultResponseModel<IEnumerable<CategoriaDTO>>(false, string.Format("Sucesso ao buscar categorias"), resultModel);
                return Ok(resultSuccess);
            }
            catch (Exception)
            {
                var resultError = new ResultResponseModel<CategoriaDTO>(true, "Erro ao buscar categorias", null);
                return BadRequest(resultError);
            }
           
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CategoriaDTO categoria)
        {
            try
            {
                if (categoria == null) return BadRequest();

                if (categoria.CatId <= 0)
                {
                    #region Create

                    var result = await _categoriaBusiness.Create(categoria);
                    var selecionarCategoriaCadastrada = await _categoriaBusiness.FindById(result);

                    var resultSuccess = new ResultResponseModel<CategoriaDTO>(false, string.Format("Sucesso ao cadastrar categoria"), result, selecionarCategoriaCadastrada);
                    return Ok(resultSuccess);

                    #endregion
                }
                else
                {
                    #region Update

                    var result = await _categoriaBusiness.Update(categoria);
                    var selecionarCategoriaCadastrada = await _categoriaBusiness.FindById(categoria.CatId);

                    var resultSuccess = new ResultResponseModel<CategoriaDTO>(false, string.Format("Sucesso ao atualizar categoria"), selecionarCategoriaCadastrada.CatId, selecionarCategoriaCadastrada);
                    return Ok(resultSuccess);

                    #endregion
                }

            }
            catch (Exception)
            {
                var resultError = new ResultResponseModel<CategoriaDTO>(true, "Erro ao cadatrar categoria", null);
                return BadRequest(resultError);
            }
        }

    }
}
