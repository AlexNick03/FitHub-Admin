using FitHubAdmin.Data;
using FitHubAdmin.DTOs;
using FitHubAdmin.Models;
using Microsoft.EntityFrameworkCore;

namespace FitHubAdmin.Services
{
    public class AccesService
    {
        private readonly AppDbContext _context;

        public AccesService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CheckInResponseDto> ProceseazaCheckInAsync(CheckInDto dto)
        {
            var client = await _context.Clienti
                .Include(c => c.Abonamente)
                .FirstOrDefaultAsync(c => c.Id == dto.ClientId);

            if (client == null) throw new KeyNotFoundException("Clientul nu exista!");

            // Verificam daca are abonament valid (neexpirat)
            var abonamentActiv = client.Abonamente
                .FirstOrDefault(a => a.DataExpirare > DateTime.Now);

            var raspuns = new CheckInResponseDto
            {
                NumeClient = client.Nume,
                DataOra = DateTime.Now
            };

            if (abonamentActiv != null)
            {
                raspuns.StatusAcces = "PERMIS";
                raspuns.Mesaj = $"Abonament {abonamentActiv.Tip} valid.";
                
                // Salvam in baza de date doar intrarile valide
                _context.IstoricAcces.Add(new IstoricAcces 
                { 
                    ClientId = client.Id, 
                    DataAcces = DateTime.Now 
                });
                await _context.SaveChangesAsync();
            }
            else
            {
                raspuns.StatusAcces = "RESPINS";
                raspuns.Mesaj = "Abonament expirat sau inexistent.";
            }

            return raspuns;
        }
    }
}