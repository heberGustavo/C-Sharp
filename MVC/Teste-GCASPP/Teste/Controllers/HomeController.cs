using Teste.Domain.Business;
using Teste.Domain.IBusiness;
using Teste.Domain.Models.EntityDomain;
using Teste.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Teste.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFuncionarioBusiness _funcionarioBusiness;

        public HomeController(IFuncionarioBusiness funcionarioBusiness)
        {
            _funcionarioBusiness = funcionarioBusiness;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
        
        public async Task<PartialViewResult> ListarDadosFuncionario()
        {
            var resultado = await _funcionarioBusiness.ObterTodosFuncionarios();
            return PartialView("ListarDadosFuncionario", resultado);
        }

    }
}
