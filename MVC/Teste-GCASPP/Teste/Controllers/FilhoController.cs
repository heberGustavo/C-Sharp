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
    public class FilhoController : Controller
    {
        private readonly IFilhoBusiness _filhoBusiness;
        private readonly IFuncionarioBusiness _funcionarioBusiness;

        public FilhoController(IFilhoBusiness filhoBusiness, IFuncionarioBusiness funcionarioBusiness)
        {
            _filhoBusiness = filhoBusiness;
            _funcionarioBusiness = funcionarioBusiness;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Funcionarios = await _funcionarioBusiness.ObterTodosFuncionarios();
            return View();
        }

        public async Task<PartialViewResult> ListarDadosFilho()
        {
            var resultado = await _filhoBusiness.ObterTodosFilhos();
            return PartialView("ListarDadosFilho", resultado);
        }

        [HttpPost]
        public async Task<JsonResult> Cadastrar(string nome, string data, int idFuncionario)
        {
            try
            {
                var model = new Filho
                {
                    nome = nome,
                    data_de_nascimento = Convert.ToDateTime(data),
                    id_funcionario = idFuncionario
                };

                var resultado = await _filhoBusiness.Cadastrar(model);

                return Json(new { erro = resultado.erro, mensagem = resultado.mensagem });

            }
            catch (Exception e)
            {
                return Json(new { erro = true, mensagem = "Ocorreu um erro. Contate o administrador." });
            }
        }

        [HttpPost]
        public async Task<JsonResult> Editar(int id, string nome, string data, int idFuncionario)
        {
            try
            {
                var model = new Filho
                {
                    id = id,
                    nome = nome,
                    data_de_nascimento = Convert.ToDateTime(data),
                    id_funcionario = idFuncionario
                };

                var resultado = await _filhoBusiness.Editar(model);

                return Json(new { erro = resultado.erro, mensagem = resultado.mensagem });

            }
            catch (Exception e)
            {
                return Json(new { erro = true, mensagem = "Ocorreu um erro. Contate o administrador." });
            }
        }

        [HttpGet]
        public async Task<JsonResult> Excluir(int id)
        {
            try
            {
                var result = await _filhoBusiness.Excluir(id);
                return Json(new { erro = result.erro, mensagem = result.mensagem });
            }
            catch
            {
                return Json(new { erro = true, mensagem = "Ocorreu um erro. Contate o administrador." });
            }
        }

        [HttpGet]
        public async Task<JsonResult> ObterFilhoPorId(int id)
        {
            try
            {
                var result = await _filhoBusiness.ObterFilhoPorId(id);
                return Json(result);
            }
            catch
            {
                return Json("Ocorreu um erro. Contate o administrador.");
            }
        }
    }
}
