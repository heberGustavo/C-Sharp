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
    public class FuncionarioController : Controller
    {
        private readonly IFuncionarioBusiness _funcionarioBusiness;
        private readonly IFilhoBusiness _filhoBusiness;

        public FuncionarioController(IFuncionarioBusiness funcionarioBusiness, IFilhoBusiness filhoBusiness)
        {
            _funcionarioBusiness = funcionarioBusiness;
            _filhoBusiness = filhoBusiness;
        }

        #region Write

        [HttpPost]
        public async Task<JsonResult> Cadastrar(string nome, string data, decimal salario)
        {
            try
            {
                //var result = await _funcionarioBusiness.VerificaSeExisteCadastrado(nome, salario);

                var model = new Funcionario
                {
                    nome = nome,
                    data_de_nascimento = Convert.ToDateTime(data),
                    salario = salario
                };

                var resultado = await _funcionarioBusiness.Cadastrar(model);

                return Json(new { erro = resultado.erro, mensagem = resultado.mensagem });

            }
            catch (Exception e)
            {
                return Json(new { erro = true, mensagem = "Ocorreu um erro. Contate o administrador." });
            }
        }

        [HttpPost]
        public async Task<JsonResult> Editar(int id, string nome, string data, decimal salario)
        {
            try
            {
                //var result = await _funcionarioBusiness.VerificaSeExisteCadastrado(nome, salario);

                var model = new Funcionario
                {
                    id = id,
                    nome = nome,
                    data_de_nascimento = Convert.ToDateTime(data),
                    salario = salario
                };

                var resultado = await _funcionarioBusiness.Editar(model);

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
                var listaFilho = await _filhoBusiness.VerificaSeExisteFilhoCadastrado(id);

                if (listaFilho >= 1)
                {
                    return Json(new { erro = true, mensagem = "Não é possivel excluir um funcionário com algum filho cadastrado. Verifique e tente novamente!" });
                }

                var result = await _funcionarioBusiness.Excluir(id);
                return Json(new { erro = result.erro, mensagem = result.mensagem });
            }
            catch (Exception e)
            {
                return Json(new { erro = true, mensagem = "Ocorreu um erro. Contate o administrador." });
            }
        }

        #endregion

        #region Read

        [HttpGet]
        public async Task<IEnumerable<Funcionario>> ObterTodosFuncionarios()
            => await _funcionarioBusiness.ObterTodosFuncionarios();

        [HttpGet]
        public async Task<JsonResult> ObterFuncionarioPorId(int id)
        {
            try
            {
                var result = await _funcionarioBusiness.ObterFuncionarioPorId(id);
                return Json(result);
            }
            catch
            {
                return Json("Ocorreu um erro. Contate o administrador.");
            }
        }

        #endregion

    }
}
