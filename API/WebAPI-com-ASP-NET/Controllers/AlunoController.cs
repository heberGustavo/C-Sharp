using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartSchool_WEBAPI.Data;
using SmartSchool_WEBAPI.Models;

namespace SmartSchool_WEBAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        public IRepository _IRepository { get; }
        public AlunoController(IRepository IRepository)
        {
            _IRepository = IRepository;

        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _IRepository.GetAllAlunosAsync(true);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }

        [HttpGet("{AlunoId}")]
        public async Task<IActionResult> GetByAlunoId(int AlunoId)
        {
            try
            {
                var result = await _IRepository.GetAlunoAsyncById(AlunoId, true);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }

        [HttpGet("ByDisciplina/{disciplinaId}")]
        public async Task<IActionResult> GetByDisciplinaId(int disciplinaId)
        {
            try
            {
                var result = await _IRepository.GetAlunosAsyncByDisciplinaId(disciplinaId, false);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> post(Aluno model)
        {
            try
            {
                _IRepository.Add(model);

                if (await _IRepository.SaveChangesAsync())
                {
                    return Ok(model);
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }

            return BadRequest();
        }

        [HttpPut("{alunoId}")]
        public async Task<IActionResult> put(int alunoId, Aluno model)
        {
            try
            {
                var aluno = await _IRepository.GetAlunoAsyncById(alunoId, false);

                //Verifica se encontrou o aluno
                if (aluno == null) return NotFound("Aluno não encontrado!");

                _IRepository.Update(model);

                if (await _IRepository.SaveChangesAsync())
                {
                    return Ok(model);
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }

            return BadRequest();
        }

        [HttpDelete("{alunoId}")]
        public async Task<IActionResult> delete(int alunoId)
        {
            try
            {
                var aluno = await _IRepository.GetAlunoAsyncById(alunoId, false);

                //Verifica se encontrou o aluno
                if (aluno == null) return NotFound("Aluno não encontrado!");

                _IRepository.Delete(aluno);

                if (await _IRepository.SaveChangesAsync())
                {
                    return Ok("Deletado!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }

            return BadRequest();
        }
    }

}