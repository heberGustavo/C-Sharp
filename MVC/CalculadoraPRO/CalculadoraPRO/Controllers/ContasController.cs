using CalculadoraPRO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CalculadoraPRO.Controllers
{
    public class ContasController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Contas Cadastradas";

            return View();
        }

        public ViewResult Listar()
        {
            return View();
        }
        
    }
}
