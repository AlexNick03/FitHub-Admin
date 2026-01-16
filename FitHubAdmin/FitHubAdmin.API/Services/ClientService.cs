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
            // 1. Luam toti clientii din DB impreuna cu abonamentele lor
            var clienti = await _context.Clienti
                .Include(c => c.Abonamente)
                .ToListAsync();

            // 2. Procesam datele in memorie (mai sigur pentru logica complexa)
            var listaRaspuns = new List<ClientResponseDto>();

            foreach (var c in clienti)
            {
                // Cautam daca exista un abonament care expira in viitor (deci e valid)
                var abonamentActiv = c.Abonamente
                    .Where(a => a.DataExpirare > DateTime.Now) // Doar cele valide
                    .OrderByDescending(a => a.DataExpirare)    // Cel mai recent (daca are mai multe)
                    .FirstOrDefault();

                listaRaspuns.Add(new ClientResponseDto
                {
                    Id = c.Id,
                    Nume = c.Nume,
                    Email = c.Email,
            
                    // Logica Activ/Inactiv
                    StatusAbonament = abonamentActiv != null ? "Activ" : "Inactiv",
            
                    // Logica Tip (Daca e activ, scriem tipul, altfel "-")
                    TipAbonament = abonamentActiv != null ? abonamentActiv.Tip : "-"
                });
            }

            return listaRaspuns;
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