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
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace AdministrativoImperial.Controllers
{
    public class DiasTrabalhadosController : Controller
    {
        private readonly IDiaTrabalhadoBusiness _diaTrabalhadoBusiness;
        private readonly IObraBusiness _obraBusiness;
        private readonly IFuncionarioBusiness _funcionarioBusiness;

        public DiasTrabalhadosController(IDiaTrabalhadoBusiness diaTrabalhadoBusiness, IObraBusiness iObraBusiness, IFuncionarioBusiness iFuncionarioBusiness)
        {
            _diaTrabalhadoBusiness = diaTrabalhadoBusiness;
            _obraBusiness = iObraBusiness;
            _funcionarioBusiness = iFuncionarioBusiness;
        }

        public IActionResult Index()
        {
            ViewBag.Funcionarios = _funcionarioBusiness.ObterCadastrados().Result.Items;
            ViewBag.Obras = _obraBusiness.ObterCadastrados().Result.Items;

            return View();
        }

        #region Read

        public async Task<IActionResult> Listar()
        {
            var result = await _diaTrabalhadoBusiness.ObterCadastrados();
            return View("Listar", result.Items);
        }

        [HttpGet]
        [Route("[controller]/[action]/{ditId:int}")]
        public async Task<JsonResult> Selecionar(int ditId)
        {
            var result = await _diaTrabalhadoBusiness.Selecionar(ditId);
            if (result.Type != ResultType.CompleteExecution)
                return Json(new { erro = true, mensagem = result.Messages });

            return Json(new { erro = false, mensagem = result.Messages, data = result.Item });
        }

        #endregion

        #region Write

        [HttpPost]
        [Route("[controller]/[action]")]
        public async Task<JsonResult> Cadastrar([FromBody] DiaTrabalhadoDTO diaTrabalhadoDTO)
        {
            var result = await _diaTrabalhadoBusiness.Cadastrar(diaTrabalhadoDTO);
            if (result.Type != ResultType.CompleteExecution)
                return Json(new { erro = true, mensagem = result.Messages });

            return Json(new { erro = false, mensagem = result.Messages });
        }

        [HttpGet]
        [Route("[controller]/[action]/{ditId:int}")]
        public async Task<JsonResult> Deletar(int ditId)
        {
            var result = await _diaTrabalhadoBusiness.Deletar(ditId);
            if (result.Type != ResultType.CompleteExecution)
                return Json(new { erro = true, mensagem = result.Messages });

            return Json(new { erro = false, mensagem = result.Messages });
        }

        #endregion

    }
}
