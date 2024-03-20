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
    public class FormasDePagamentoController : Controller
    {
        private readonly ITipoFormasDePagamentoBusiness _tipoFormasDePagamentoBusiness;
        private readonly IFormasDePagamentoBusiness _formasDePagamentoBusiness;

        public FormasDePagamentoController(ITipoFormasDePagamentoBusiness tipoFormasDePagamentoBusiness, IFormasDePagamentoBusiness formasDePagamentoBusiness)
        {
            _tipoFormasDePagamentoBusiness = tipoFormasDePagamentoBusiness;
            _formasDePagamentoBusiness = formasDePagamentoBusiness;
        }

        public async Task<IActionResult> Index()
        {
            var resultado = await _formasDePagamentoBusiness.ObterTodasFormasDePagamentoAtivo();
            return View(resultado);
        }

        public async Task<IActionResult> Novo()
        {
            ViewBag.TipoFormaDePagamento = await _tipoFormasDePagamentoBusiness.ObterTodasFormasDePagamentoAtiva();
            return View();
        }

        [HttpPost]
        [Route("[controller]/[action]")]
        public async Task<JsonResult> CriarNovaFormaDePagamento([FromBody] FormasDePagamento body)
        {
            var resultado = await _formasDePagamentoBusiness.CriarNovaFormaDePagamento(body);
            return Json(new { erro = resultado.erro, mensagem = resultado.mensagem });
        }

        [HttpGet]
        [Route("[controller]/[action]/{idFormaDePagamento:int}")]
        public async Task<IActionResult> Editar(int idFormaDePagamento)
        {
            ViewBag.TipoFormaDePagamento = await _tipoFormasDePagamentoBusiness.ObterTodasFormasDePagamentoAtiva();

            var resultado = await _formasDePagamentoBusiness.ObterFormaDePagamentoPorId(idFormaDePagamento);
            return View(resultado);
        }

        [HttpPost]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> EditarFormaDePagamento([FromBody] FormasDePagamento body)
        {
            var resultado = await _formasDePagamentoBusiness.EditarFormaDePagamento(body);
            return Json(new { erro = resultado.erro, mensagem = resultado.mensagem });
        }

        [HttpGet]
        [Route("[controller]/[action]/{idFormaDePagamento:int}")]
        public async Task<JsonResult> ExcluirFormaDePagamento(int idFormaDePagamento)
        {
            var resultado = await _formasDePagamentoBusiness.ExcluirFormaDePagamento(idFormaDePagamento);

            if (resultado == 1)
                return Json(new { erro = false, mensagem = "Forma de Pagamento excluída com sucesso!" });
            else
                return Json(new { erro = false, mensagem = "Erro ao excluir Forma de Pagamento!" });
        }

        [HttpGet]
        [Route("[controller]/[action]")]
        public async Task<JsonResult> ObterTodasFormasDePagamentoAtivo()
        {
            var resultado = await _formasDePagamentoBusiness.ObterTodasFormasDePagamentoAtivo();
            return Json(resultado);
        }

    }
}
