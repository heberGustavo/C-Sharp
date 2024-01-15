using AdministrativoImperial.Domain.IBusiness;
using AdministrativoImperial.Domain.Models.EntityDomain;
using Gpnet.Common.ExecutionManager;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using @BCryptNet = BCrypt.Net;

namespace AdministrativoImperial.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioBusiness _usuarioBusiness;

        public UsuarioController(IUsuarioBusiness usuarioBusiness)
        {
            _usuarioBusiness = usuarioBusiness;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Write

        [HttpPost]
        [Route("[controller]/[action]")]
        public async Task<JsonResult> Cadastrar([FromBody] UsuarioDTO usuario)
        {
            if (ModelState.IsValid)
            {
                var resultado = await _usuarioBusiness.Cadastrar(usuario);

                if (resultado.Type != ResultType.CompleteExecution)
                    return Json(new { erro = true, mensagem = resultado.Messages });

                return Json(new { erro = false, mensagem = resultado.Messages });
            }
            else
            {
                var erros = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return Json(new { erro = true, mensagem = erros });
            }

        }

        [HttpGet]
        [Route("[controller]/[action]/{usaId:int}")]
        public async Task<JsonResult> Deletar(int usaId)
        {
            var result = await _usuarioBusiness.Deletar(usaId);
            if (result.Type != ResultType.CompleteExecution)
                return Json(new { erro = true, mensagem = result.Messages });

            return Json(new { erro = false, mensagem = result.Messages });
        }

        #endregion

        #region Read

        [HttpGet]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> Listar()
        {
            var resultado = await _usuarioBusiness.Listar();
            return View("Listar", resultado.Items);
        }

        [HttpGet]
        [Route("[controller]/[action]/{usaId:int}")]
        public async Task<JsonResult> Selecionar(int usaId)
        {
            var result = await _usuarioBusiness.Selecionar(usaId);
            if(result.Type != ResultType.CompleteExecution)
                return Json(new { erro = true, mensagem = result.Messages });

            return Json(new { erro = false, mensagem = result.Messages, data = result.Item });
        }

        #endregion

    }
}
