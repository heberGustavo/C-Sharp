using AdministrativoImperial.Domain.IBusiness;
using AdministrativoImperial.Domain.Models.EntityDomain;
using AdministrativoImperial.Models;
using Gpnet.Common.ExecutionManager;
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

            return View();
        }

        #region Write

        [HttpPost]
        [Route("[controller]/[action]")]
        public async Task<JsonResult> Cadastrar([FromBody] FuncionarioDTO funcioanrio)
        {
            var resultado = await _funcionarioBusiness.Cadastrar(funcioanrio);
            
            if (resultado.Type != ResultType.CompleteExecution)
                return Json(new { erro = true, mensagem = resultado.Messages });

            return Json(new { erro = false, mensagem = resultado.Messages });
        }

        [HttpGet]
        [Route("[controller]/[action]/{funId:int}")]
        public async Task<JsonResult> Desativar(int funId)
        {
            var result = await _funcionarioBusiness.Desativar(funId);

            if (result.Type != ResultType.CompleteExecution)
                return Json(new { erro = true, mensagem = result.Messages });

            return Json(new { erro = false, mensagem = result.Messages });
        }

        #endregion

        #region Read

        [HttpGet]
        public async Task<ViewResult> Listar()
        {
            var result = await _funcionarioBusiness.ObterCadastrados();
            return View(result.Items);
        }

        [HttpGet]
        [Route("[controller]/[action]")]
        public async Task<JsonResult> ObterTodosFuncionarios()
        {
            var resultado = await _funcionarioBusiness.ObterCadastrados();
            return Json(new { resultado });
        }

        [HttpGet]
        [Route("[controller]/[action]/{funId:int}")]
        public async Task<JsonResult> Selecionar(int funId)
        {
            var result = await _funcionarioBusiness.Selecionar(funId);
            return Json(new { result.Item });
        }

        #endregion

    }
}
