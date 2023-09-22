using AdministrativoImperial.Domain.IBusiness;
using AdministrativoImperial.Domain.Models.EntityDomain;
using AdministrativoImperial.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AdministrativoImperial.Controllers
{
    public class FuncionarioController : Controller
    {
        private readonly IFuncionarioBusiness _funcionarioBusiness;
        private readonly IFuncaoFuncionarioBusiness _funcaoFuncionarioBusiness;

        public FuncionarioController(IFuncionarioBusiness funcionarioBusiness, IFuncaoFuncionarioBusiness funcaoFuncionarioBusiness)
        {
            _funcionarioBusiness = funcionarioBusiness;
            _funcaoFuncionarioBusiness = funcaoFuncionarioBusiness;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.FuncaoFuncionario = await _funcaoFuncionarioBusiness.ObterCadastradosAtivos();

            var result = await _funcionarioBusiness.ObterCadastrados();
            return View(result);
        }

        [HttpPost]
        [Route("[controller]/[action]")]
        public async Task<JsonResult> Cadastrar([FromBody] FuncionarioDTO funcioanrio)
        {
            var resultado = await _funcionarioBusiness.Cadastrar(funcioanrio);
            return Json(new { erro = resultado.erro, mensagem = resultado.mensagem } );
        }

        [HttpGet]
        [Route("[controller]/[action]")]
        public async Task<JsonResult> ObterTodosFuncionarios()
        {
            var resultado = await _funcionarioBusiness.ObterCadastrados();
            return Json(new { resultado });
        }

    }
}
