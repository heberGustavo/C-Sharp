using AdministrativoImperial.Domain.IBusiness;
using AdministrativoImperial.Domain.Models.Common;
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
    public class FuncaoFuncionarioController : Controller
    {
        private readonly IFuncaoFuncionarioBusiness _funcaoFuncionarioBusiness;

        public FuncaoFuncionarioController(IFuncaoFuncionarioBusiness funcaoFuncionarioBusiness)
        {
            _funcaoFuncionarioBusiness = funcaoFuncionarioBusiness;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _funcaoFuncionarioBusiness.ObterCadastrados();
            return View(result);
        }

        #region Writer

        [HttpPost]
        [Route("[controller]/[action]")]
        public async Task<JsonResult> Cadastrar([FromBody] FuncaoFuncionarioDTO funcaoFuncionario)
        {
            var resultado = await _funcaoFuncionarioBusiness.Cadastrar(funcaoFuncionario);
            return Json(new { erro = resultado.erro, mensagem = resultado.mensagem });
        }

        [HttpGet]
        [Route("[controller]/[action]/{id:int}")]
        public async Task<JsonResult> Deletar(int id)
        {
            var resultado = await _funcaoFuncionarioBusiness.Deletar(id);
            return Json(new { erro = resultado.erro, mensagem = resultado.mensagem });
        }

        #endregion

        #region Read

        [HttpGet]
        [Route("[controller]/[action]")]
        public async Task<JsonResult> ObterTodasFuncoes()
        {
            var resultado = await _funcaoFuncionarioBusiness.ObterCadastrados();
            return Json(new { resultado });
        }

        #endregion
      
    }
}
