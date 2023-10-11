using AdministrativoImperial.Domain.IBusiness;
using AdministrativoImperial.Domain.Models.Common;
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
    public class MaterialController : Controller
    {
        private readonly IMaterialBusiness _materialBusiness;

        public MaterialController(IMaterialBusiness materialBusiness)
        {
            _materialBusiness = materialBusiness;
        }

        public IActionResult Index()
        {
            ViewBag.Titulo = "Lista de Materiais";
            return View();
        }

        #region Writer

        [HttpPost]
        [Route("[controller]/[action]")]
        public async Task<JsonResult> Cadastrar([FromBody] MaterialDTO model)
        {
            if (ModelState.IsValid)
            {
                model.ObrId = 7;
                var resultado = await _materialBusiness.Create(model);

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
        [Route("[controller]/[action]/{id:int}")]
        public async Task<JsonResult> Deletar(int id)
        {
            var resultado = await _materialBusiness.Deletar(id);

            if (resultado.Type != ResultType.CompleteExecution)
                return Json(new { erro = true, mensagem = resultado.Messages });

            return Json(new { erro = false, mensagem = resultado.Messages });
        }

        #endregion

        #region Read


        [HttpGet]
        public async Task<ViewResult> Listar()
        {
            var result = await _materialBusiness.GetAllAsync();
            return View("Listar", result.Items);
        }

        #endregion
      
    }
}
