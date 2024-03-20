using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sorteio.Domain.IBusiness;
using Sorteio.Domain.Models.EntityDomain;
using Sorteio.Models;
using Sorteio.Portal.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Sorteio.Controllers
{
    [Authorize(Policy = PolicyKeys.USUARIO_LOGADO_ADM)]
    public class CategoriaSorteioController : Controller
    {
       private readonly ICategoriaSorteioBusiness _categoriaSorteioBusiness;

        public CategoriaSorteioController(ICategoriaSorteioBusiness categoriaSorteioBusiness)
        {
            _categoriaSorteioBusiness = categoriaSorteioBusiness;
        }

        public async Task<IActionResult> Index()
        {
            var resultado = await _categoriaSorteioBusiness.ObterTodosCategoriaSorteioAtivo();
            return View(resultado);
        }

        [HttpGet]
        [Route("[controller]/[action]")]
        public async Task<JsonResult> ObterTodosCategoriaSorteioAtivo()
        {
            var resultado = await _categoriaSorteioBusiness.ObterTodosCategoriaSorteioAtivo();
            return Json(resultado);
        }

        [HttpPost]
        [Route("[controller]/[action]")]
        public async Task<JsonResult> CriarCategoriaSorteio([FromBody] CategoriaSorteio body)
        {
            var resultado = await _categoriaSorteioBusiness.CriarCategoriaSorteio(body);
            return Json(new { erro = resultado.erro, mensagem = resultado.mensagem });
        }

        [HttpPost]
        [Route("[controller]/[action]")]
        public async Task<JsonResult> EditarCategoriaSorteio([FromBody] CategoriaSorteio body)
        {
            var resultado = await _categoriaSorteioBusiness.EditarCategoriaSorteio(body);
            return Json(new { erro = resultado.erro, mensagem = resultado.mensagem });
        }

        [HttpGet]
        [Route("[controller]/[action]/{idCategoriaSorteio:int}")]
        public async Task<JsonResult> ExcluirCategoriaSorteio(int idCategoriaSorteio)
        {
            var resultado = await _categoriaSorteioBusiness.ExcluirCategoriaSorteio(idCategoriaSorteio);

            if (resultado == 1)
                return Json(new { erro = false, mensagem = "Categoria excluída com sucesso!" });
            else
                return Json(new { erro = false, mensagem = "Erro ao excluir categoria!" });
        }
    }
}
