using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sorteio.Domain.IBusiness;
using Sorteio.Domain.Models.Body;
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
    public class SorteiosController : Controller
    {
        private readonly ICategoriaSorteioBusiness _categoriaSorteioBusiness;
        private readonly ISorteiosBusiness _sorteiosBusiness;
        private readonly IUsuarioBusiness _usuarioBusiness;

        public SorteiosController(ICategoriaSorteioBusiness categoriaSorteioBusiness, ISorteiosBusiness sorteiosBusiness, IUsuarioBusiness usuarioBusiness)
        {
            _categoriaSorteioBusiness = categoriaSorteioBusiness;
            _sorteiosBusiness = sorteiosBusiness;
            _usuarioBusiness = usuarioBusiness;
        }

        [Authorize(Policy = PolicyKeys.USUARIO_LOGADO_ADM)]
        public async Task<IActionResult> Index()
        {
            ViewBag.Usuario = await _usuarioBusiness.ObterTodosUsuarios();

            var resultado = await _sorteiosBusiness.ObterTodosSorteio();
            return View(resultado);
        }

        [Authorize(Policy = PolicyKeys.USUARIO_LOGADO_ADM)]
        public async Task<IActionResult> Novo()
        {
            ViewBag.CategoriaSorteio = await _categoriaSorteioBusiness.ObterTodosCategoriaSorteioAtivo();

            return View();
        }

        [Authorize(Policy = PolicyKeys.USUARIO_LOGADO_ADM)]
        [HttpGet]
        [Route("[controller]/[action]/{idSorteio:int}")]
        public async Task<IActionResult> Editar(int idSorteio)
        {
            ViewBag.CategoriaSorteio = await _categoriaSorteioBusiness.ObterTodosCategoriaSorteioAtivo();
            ViewBag.Usuario = await _usuarioBusiness.ObterTodosUsuarios();

            var resultado = await _sorteiosBusiness.ObterSorteioPorId(idSorteio);

            return View(resultado);
        }

        [Authorize(Policy = PolicyKeys.USUARIO_LOGADO_ADM)]
        [HttpPost]
        [Route("[controller]/[action]")]
        public async Task<JsonResult> CriarNovoSorteio([FromBody] SorteioBody body)
        {
            var resultado = await _sorteiosBusiness.CriarNovoSorteio(body);

            return Json(new { erro = resultado.erro, mensagem = resultado.mensagem });
        }

        [Authorize(Policy = PolicyKeys.USUARIO_LOGADO_ADM)]
        [HttpPost]
        [Route("[controller]/[action]")]
        public async Task<JsonResult> EditarSorteio([FromBody] SorteioBody body)
        {
            var resultado = await _sorteiosBusiness.EditarSorteio(body);
            return Json(new { erro = resultado.erro, mensagem = resultado.mensagem });
        }

        [Authorize(Policy = PolicyKeys.USUARIO_LOGADO_ADM)]
        [HttpPost]
        [Route("[controller]/[action]")]
        public async Task<JsonResult> EditarFinalizarSorteio([FromBody] VencedorSorteio body)
        {
            var resultado = await _sorteiosBusiness.EditarFinalizarSorteio(body);

            if (resultado)
                return Json(new { erro = false, mensagem = "Dados atualizados!" });
            else
                return Json(new { erro = true, mensagem = "Erro ao atualizar dados. Tente novamente!" });
        }

        [Authorize(Policy = PolicyKeys.USUARIO_LOGADO_ADM)]
        [HttpPost]
        [Route("[controller]/[action]")]
        public async Task<JsonResult> FinalizarSorteio([FromBody] VencedorSorteio body)
        {
            var resultado = await _sorteiosBusiness.FinalizarSorteio(body);

            return Json(new { erro = resultado.erro, mensagem = resultado.mensagem });
        }

        [Authorize(Policy = PolicyKeys.USUARIO_LOGADO_ADM)]
        [HttpGet]
        [Route("[controller]/[action]/{idSorteio:int}")]
        public async Task<JsonResult> ExcluirSorteio(int idSorteio)
        {
            var resultado = await _sorteiosBusiness.ExcluirSorteio(idSorteio);

            if (resultado == 1)
                return Json(new { erro = false, mensagem = "Sorteio excluído com sucesso!" });
            else
                return Json(new { erro = false, mensagem = "Erro ao excluir sorteio!" });
        }

        [HttpGet]
        [Route("[controller]/[action]")]
        public async Task<JsonResult> ObterTodosSorteio()
        {
            var resultado = await _sorteiosBusiness.ObterTodosSorteio();
            return Json(new { resultado });
        }

        [HttpGet]
        [Route("[controller]/[action]")]
        public async Task<JsonResult> ObterTodosPedidosPendentes()
        {
            var resultado = await _sorteiosBusiness.ObterTodosPedidosPendentes();
            return Json(new { resultado });
        }

        [HttpGet]
        [Route("[controller]/[action]/{idCategoria}")]
        public async Task<JsonResult> FiltrarSorteioPorCategoria(int idCategoria)
        {
            var resultado = await _sorteiosBusiness.FiltrarSorteioPorCategoria(idCategoria);
            return Json(resultado);
        }

        [HttpGet]
        [Route("[controller]/[action]")]
        public async Task<JsonResult> ObterTodosUltimosSorteiosRealizados()
        {
            var resultado = await _sorteiosBusiness.ObterTodosUltimosSorteiosRealizados();
            return Json(resultado);
        }

        [HttpGet]
        [Route("[controller]/[action]/{idSorteio:int}")]
        public async Task<JsonResult> ObterParticipantesSorteioPorId(int idSorteio)
        {
            var resultado = await _sorteiosBusiness.ObterParticipantesSorteioPorId(idSorteio);
            return Json(resultado);
        }

        [HttpGet]
        [Route("[controller]/[action]/{idPedido:int}")]
        public async Task<JsonResult> VisualizarNumerosPorIdPedido(int idPedido)
        {
            var resultado = await _sorteiosBusiness.VisualizarNumerosPorIdPedido(idPedido);
            return Json(resultado);
        }

        [Authorize(Policy = PolicyKeys.USUARIO_LOGADO_ADM)]
        [HttpGet]
        [Route("[controller]/[action]/{idPedido:int}")]
        public async Task<JsonResult> ConfirmarPagamentoRecebido(int idPedido)
        {
            var resultado = await _sorteiosBusiness.ConfirmarPagamentoRecebido(idPedido);

            if (resultado == 1)
                return Json(new { erro = false, mensagem = "Pagamento confirmado com sucesso!" });
            else
                return Json(new { erro = false, mensagem = "Erro ao confirmar pagamento!" });

        }

        [HttpGet]
        [Route("[controller]/[action]/{idSorteio:int}")]
        public async Task<JsonResult> BuscarTodosNumerosSorteioPorId(int idSorteio)
        {
            var resultado = await _sorteiosBusiness.BuscarTodosNumerosSorteioPorId(idSorteio);
            return Json(resultado);
        }

        [HttpGet]
        [Route("[controller]/[action]/{idSorteio:int}/{idStatusPedido:int}")]
        public async Task<JsonResult> BuscarNumerosReservadoOuPagoSorteioPorId(int idSorteio, int idStatusPedido)
        {
            var resultado = await _sorteiosBusiness.BuscarNumerosReservadoOuPagoSorteioPorId(idSorteio, idStatusPedido);
            return Json(resultado);
        }

        [HttpGet]
        [Route("[controller]/[action]/{idUsuario:int}")]
        public async Task<JsonResult> InformacoesDoUsuarioGanhadorPorId(int idUsuario)
        {
            var resultado = await _usuarioBusiness.InformacoesDoUsuarioGanhadorPorId(idUsuario);
            return Json(resultado);
        }

        [HttpGet]
        [Route("[controller]/[action]/{idUsuario:int}")]
        public async Task<JsonResult> MostrarNumerosDoUsuario(int idUsuario)
        {
            var resultado = await _usuarioBusiness.MostrarNumerosDoUsuario(idUsuario);
            return Json(resultado);
        }

        [HttpGet]
        [Route("[controller]/[action]/{celularUsuario}/{idSorteio:int}")]
        public async Task<JsonResult> ConsultarNumerosCliente(string celularUsuario, int idSorteio)
        {
            celularUsuario = celularUsuario.Replace("%20", " ");

            var resultado = await _usuarioBusiness.ConsultarNumerosCliente(celularUsuario, idSorteio);
            return Json(resultado);
        }
    }
}