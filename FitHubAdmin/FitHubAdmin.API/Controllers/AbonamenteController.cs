using FitHubAdmin.DTOs;
using FitHubAdmin.Services;
using Microsoft.AspNetCore.Mvc;

namespace FitHubAdmin.Controllers
{
    [ApiController] 
    [Route("api/[controller]")] 
    public class AbonamenteController : ControllerBase
    {
        private readonly AbonamentService _service;

        public AbonamenteController(AbonamentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAbonamenteAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAbonamentDto dto)
        {
            try
            {
                // Incercam sa cream abonamentul
                await _service.CreateAbonamentAsync(dto);
                return Ok("Abonament creat cu succes!");
            }
            catch (KeyNotFoundException ex)
            {
                // Aici intram cand ID-ul clientului e gresit
                // Returnam 404 Not Found cu mesajul tau
                return NotFound(new { Eroare = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                // Daca Service-ul zice ca exista deja, returnam 400 Bad Request cu mesajul nostru
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Pentru orice alta eroare neprevazuta
                return StatusCode(500, "A aparut o eroare interna.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAbonamentAsync(id);
            if (!result) return NotFound("Abonamentul nu exista.");
            return Ok("Abonament sters.");
        }
    }
}