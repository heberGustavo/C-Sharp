using AdministrativoImperial.Domain.IBusiness;
using AdministrativoImperial.Domain.Models.Body;
using AdministrativoImperial.Domain.Models.EntityDomain;
using Gpnet.Common.ExecutionManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using @BCryptNet = BCrypt.Net;

namespace AdministrativoImperial.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioBusiness _usuarioBusiness;

        public LoginController(IUsuarioBusiness usuarioBusiness)
        {
            _usuarioBusiness = usuarioBusiness;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> Autenticar([FromBody] UsuarioBody usuario)
        {
            if (ModelState.IsValid)
            {
                var resultUsuario = await _usuarioBusiness.SelecionarPorEmail(usuario.email);
                if (resultUsuario.Type != ResultType.CompleteExecution)
                    return Json(new { erro = true, mensagem = resultUsuario.Messages });

                if (resultUsuario.Item != null)
                {
                    var hashSenhaArmazenada = Encoding.UTF8.GetString(resultUsuario.Item.UsaSenha);
                    var saltString = Encoding.UTF8.GetString(resultUsuario.Item.UsaSalt);
                    var hashSenhaAtual = BCryptNet.BCrypt.HashPassword(usuario.senha, saltString);

                    if (!hashSenhaAtual.Equals(hashSenhaArmazenada))
                        return Json(new { erro = true, mensagem = "Senha inválida. Verifique e tente novamente!" });

                    HttpContext.Session.SetInt32("__Autenticado", 1);
                    HttpContext.Session.SetString("__Usuario", resultUsuario.Item.UsaNome);

                    return Json(new { erro = false, mensagem = "Usuário autenticado!", infoUser = new { resultUsuario.Item.UsaId, resultUsuario.Item.UsaNome } } );
                }
                else
                    return Json(new { erro = true, mensagem = "Email não encontrado. Verifique e tente novamente!" });
            }
            else
            {
                var erros = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return Json(new { erro = true, mensagem = erros });
            }

        }

    }
}
