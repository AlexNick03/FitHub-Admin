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

            // 2. Procesam datele in memorie
            var listaRaspuns = new List<ClientResponseDto>();

            foreach (var c in clienti)
            {
                // Cautam daca exista un abonament care expira in viitor
                var abonamentActiv = c.Abonamente
                    .Where(a => a.DataExpirare > DateTime.Now) 
                    .OrderByDescending(a => a.DataExpirare)    
                    .FirstOrDefault();

                listaRaspuns.Add(new ClientResponseDto
                {
                    Id = c.Id,
                    Nume = c.Nume,
                    Email = c.Email,
            
                    // Logica Activ/Inactiv
                    StatusAbonament = abonamentActiv != null ? "Activ" : "Inactiv",
            
                    // CORECTIA ESTE AICI:
                    // Trebuie sa convertim Enum-ul (Bronze/Silver) in text folosind .ToString()
                    TipAbonament = abonamentActiv != null ? abonamentActiv.Tip.ToString() : "-" 
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