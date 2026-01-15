using FitHubAdmin.Data;
using FitHubAdmin.DTOs;
using FitHubAdmin.Models;
using Microsoft.EntityFrameworkCore;

namespace FitHubAdmin.Services
{
    public class ClientService
    {
        private readonly AppDbContext _context;

        public ClientService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ClientResponseDto>> GetAllClientsAsync()
        {
            // FARA .Include( abonamente )
            return await _context.Clienti
                .Select(c => new ClientResponseDto
                {
                    Id = c.Id,
                    Nume = c.Nume,
                    Email = c.Email,
                })
                .ToListAsync();
        }

        public async Task CreateClientAsync(CreateClientDto dto)
        {
            var clientNou = new Client
            {
                Nume = dto.Nume,
                Email = dto.Email,
                DataInregistrare = DateTime.Now
            };

            _context.Clienti.Add(clientNou);
            await _context.SaveChangesAsync();
        }
    }
}