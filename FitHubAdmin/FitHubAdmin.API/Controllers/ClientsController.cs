using FitHubAdmin.DTOs;
using FitHubAdmin.Services;
using Microsoft.AspNetCore.Mvc;

namespace FitHubAdmin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly ClientService _service;

        public ClientsController(ClientService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetClients()
        {
            return Ok(await _service.GetAllClientsAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody] CreateClientDto dto)
        {
            await _service.CreateClientAsync(dto);
            return Ok("Client creat cu succes!");
        }
    }
}