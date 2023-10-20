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
    public class LoginController : Controller
    {
        private readonly IUsuarioBusiness usuarioBusiness;
        
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> Login([FromBody] UsuarioDTO usuario)
        {
            if (ModelState.IsValid)
            {
                var resultUsuario = await usuarioBusiness.Selecionar(usuario.UsaEmail, usuario.UsaSenha);
                if (resultUsuario.Type != ResultType.CompleteExecution)
                    Console.WriteLine("mostrar erro aq");

                if(resultUsuario != null)
                {
                    var senhaSaltUsuario = usuario.UsaSenha + usuario.UsaSalt;
                    var senhaSaltUsuarioCripto = BCryptNet.BCrypt.HashPassword(senhaSaltUsuario);

                    if(resultUsuario.Item.UsaEmail.Equals(usuario.UsaEmail) && BCryptNet.BCrypt.Verify(senhaSaltUsuarioCripto, resultUsuario.Item.UsaSenha))
                        Console.WriteLine("sucesso");
                    else
                        Console.WriteLine("email ou senha incorreto");
                }
                else
                {
                    //Usuario nao foi encontrado
                }
            }
            return View();
        }

        [HttpPost]
        [Route("[controller]/[action]")]
        public async Task<JsonResult> Cadastrar([FromBody] UsuarioDTO usuario)
        {
            if (ModelState.IsValid)
            {
                var resultado = await usuarioBusiness.Create(usuario);

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
    }
}
