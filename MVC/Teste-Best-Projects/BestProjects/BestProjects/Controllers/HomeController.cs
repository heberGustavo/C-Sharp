using BestProjects.Domain.Business;
using BestProjects.Domain.IBusiness;
using BestProjects.Domain.Models.EntityDomain;
using BestProjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BestProjects.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUsuarioBusiness _usuarioBusiness;

        public HomeController(IUsuarioBusiness usuarioBusiness)
        {
            _usuarioBusiness = usuarioBusiness;
        }

        public async Task<IActionResult> Index()
        {
            var resultado = await _usuarioBusiness.ObterTodosContatos();
            return View(resultado);
        }

        [HttpGet]
        [Route("[controller]/[action]")]
        public async Task<JsonResult> ObterTodosContatos()
        {
            var resultado = await _usuarioBusiness.ObterTodosContatos();
            return Json(new { erro = false, resultado });
        }

        [HttpPost]
        [Route("[controller]/[action]")]
        public async Task<JsonResult> CadastrarContato([FromBody] Usuario usuario)
        {
            var resultado = await _usuarioBusiness.CadastrarContato(usuario);
            return Json(new { erro = resultado.erro, mensagem = resultado.mensagem });
        }

        [HttpGet]
        [Route("[controller]/[action]/{idContato}")]
        public async Task<JsonResult> ExcluirContato(int idContato)
        {
            var resultado = await _usuarioBusiness.ExcluirContato(idContato);
            return Json(new { erro = resultado.erro, mensagem = resultado.mensagem });
        }
    }
}
