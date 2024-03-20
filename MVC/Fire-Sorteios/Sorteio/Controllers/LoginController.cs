using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
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
    public class LoginController : Controller
    {
        private static IHttpContextAccessor _httpContextAccessor;
        public static void Configure(IHttpContextAccessor httpContextAccessor) => _httpContextAccessor = httpContextAccessor;

        private readonly IUsuarioBusiness _usuarioBusiness;

        public LoginController(IUsuarioBusiness usuarioBusiness)
        {
            _usuarioBusiness = usuarioBusiness;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> CadastrarUsuarioLogin([FromBody] Usuario body)
        {
            var resultadoCadastro = await _usuarioBusiness.CriarUsuario(body);

            return Json(new { erro = resultadoCadastro.erro, mensagem = resultadoCadastro.mensagem });
        }

        public async Task<JsonResult> CadastrarUsuarioCadastrarNumeros([FromBody] CadastrarUsuarioListaNumerosBody body)
        {
            var resultadoCadastro = await _usuarioBusiness.CadastrarUsuarioCadastrarNumeros(body);

            return Json(new { erro = resultadoCadastro.erro, mensagem = resultadoCadastro.mensagem, resultadoCadastro.id_cadastrado, resultadoCadastro.model });
        }

        [HttpPost]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> Login([FromBody] LoginBody loginBody)
        {
            var resultLogin = await _usuarioBusiness.RealizarLogin(loginBody.email, loginBody.senha);

            if (!resultLogin.erro)
            {
                var claimUsuario = AuthHelper.GerarClaimsUsuarioLogado(resultLogin.model);

                await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                                                    claimUsuario, new AuthenticationProperties());
            }

            return Json(new { erro = resultLogin.erro, mensagem = resultLogin.mensagem, model = resultLogin.model });
        }

        [HttpPost]
        [Route("[controller]/[action]")]
        public async Task<JsonResult> LogarCadastraNumeros([FromBody] LoginListaNumerosBody login)
        {
            var resultLogin = await _usuarioBusiness.LogarCadastraNumeros(login);

            if (!resultLogin.erro)
            {
                var claimUsuario = AuthHelper.GerarClaimsUsuarioLogado(resultLogin.model);

                await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                                                    claimUsuario, new AuthenticationProperties());
            }

            return Json(new { erro = resultLogin.erro, mensagem = resultLogin.mensagem, model = resultLogin.model });
        }

        [Route("[controller]/[action]")]
        public async Task<IActionResult> Logout()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

    }
}
