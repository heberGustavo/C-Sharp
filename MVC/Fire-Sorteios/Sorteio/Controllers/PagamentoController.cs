using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sorteio.Domain.IBusiness;
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
    [Authorize(Policy = PolicyKeys.USUARIO_LOGADO_ADM)]
    public class PagamentoController : Controller
    {
        private readonly ISorteiosBusiness _sorteiosBusiness;

        public PagamentoController(ISorteiosBusiness sorteiosBusiness)
        {
            _sorteiosBusiness = sorteiosBusiness;
        }

        public async Task<IActionResult> Index()
        {
            var resultado = await _sorteiosBusiness.ObterTodosSorteioAtivos();
            return View(resultado);
        }
    }
}
