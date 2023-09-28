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
    public class ObraController : Controller
    {
        private readonly IObraBusiness _obraBusiness;

        public ObraController(IObraBusiness obraBusiness)
        {
            _obraBusiness = obraBusiness;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Write

        [HttpPost]
        [Route("[controller]/[action]")]
        public async Task<JsonResult> Cadastrar([FromBody] ObraDTO obra)
        {
            var result = await _obraBusiness.Cadastrar(obra);

            if (result.Type != ResultType.CompleteExecution)
                return Json(new { erro = true, mensagem = result.Messages }) ;

            return Json(new { erro = false, mensagem = result.Messages });
        }

        #endregion

        #region Read

        [Route("[controller]/[action]")]
        public async Task<ViewResult> Listar()
        {
            var result = await _obraBusiness.ObterCadastrados();
            return View("Listar", result.Items);
        }

        [HttpGet]
        [Route("[controller]/[action]")]
        public async Task<JsonResult> ObterTodasObras()
        {
            var resultado = await _obraBusiness.ObterCadastrados();
            return Json(new { resultado });
        }

        #endregion
    }
}
