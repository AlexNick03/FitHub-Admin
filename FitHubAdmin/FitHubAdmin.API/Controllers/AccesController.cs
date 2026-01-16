using FitHubAdmin.DTOs;
using FitHubAdmin.Services;
using Microsoft.AspNetCore.Mvc;

namespace FitHubAdmin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccesController : ControllerBase
    {
        private readonly AccesService _service;

        public AccesController(AccesService service)
        {
            _service = service;
        }

        [HttpPost("CheckIn")]
        public async Task<IActionResult> CheckIn([FromBody] CheckInDto dto)
        {
            try
            {
                var rezultat = await _service.ProceseazaCheckInAsync(dto);
                return Ok(rezultat);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Eroare interna: " + ex.Message);
            }
        }
    }
}