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
            await _service.CreateAbonamentAsync(dto);
            return Ok("Abonament creat cu succes!");
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