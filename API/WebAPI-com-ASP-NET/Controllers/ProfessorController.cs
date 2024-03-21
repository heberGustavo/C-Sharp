using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartSchool_WEBAPI.Data;
using SmartSchool_WEBAPI.Models;

namespace SmartSchool_WEBAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorController : ControllerBase
    {
        public IRepository _IRepository { get; }
        public ProfessorController(IRepository IRepository)
        {
            _IRepository = IRepository;
        }

        [HttpGet]
        public async Task<IActionResult> get()
        {
            try
            {
                var result = await _IRepository.GetAllProfessoresAsync(true);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }

        [HttpGet("{professorId}")]
        public async Task<IActionResult> getById(int professorId)
        {
            try
            {
                var result = await _IRepository.GetProfessorAsyncById(professorId, true);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }

        [HttpGet("ByAluno/{alunoId}")]
        public async Task<IActionResult> getByAlunoId(int alunoId)
        {
            try
            {
                var result = await _IRepository.GetProfessoresAsyncByAlunoId(alunoId, true);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> post(Professor model)
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

        [HttpPut("{professorId}")]
        public async Task<IActionResult> put(int professorId, Professor model)
        {
            try
            {
                var professor = await _IRepository.GetProfessorAsyncById(professorId, false);

                //Verifica se encontrou o Professor
                if (professor == null) return NotFound("Professor não encontrado!");

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

        [HttpDelete("{professorId}")]
        public async Task<IActionResult> delete(int professorId)
        {
            try
            {
                var professor = await _IRepository.GetProfessorAsyncById(professorId, false);

                //Verifica se encontrou o Professor
                if (professor == null) return NotFound("Professor não encontrado!");

                _IRepository.Delete(professor);

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