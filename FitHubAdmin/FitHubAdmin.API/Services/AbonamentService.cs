using FitHubAdmin.Data;
using FitHubAdmin.DTOs;
using FitHubAdmin.Models;
using Microsoft.EntityFrameworkCore;

namespace FitHubAdmin.Services
{
    public class AbonamentService
    {
        private readonly AppDbContext _context;

        public AbonamentService(AppDbContext context)
        {
            _context = context;
        }

        // 1. GET: Aici facem legatura (Join)
        public async Task<List<AbonamentResponseDto>> GetAllAbonamenteAsync()
        {
            return await _context.Abonamente
                .Include(a => a.Client) // <--- IMPORTANT: Aduce datele clientului
                .Select(a => new AbonamentResponseDto
                {
                    Id = a.Id,
                    Tip = a.Tip,
                    Pret = a.Pret,
                    DataStart = a.DataStart,
                    DataExpirare = a.DataExpirare,

                    // Mapam datele:
                    ClientId = a.ClientId,
                    NumeClient = a.Client != null ? a.Client.Nume : "Client Sters"
                })
                .ToListAsync();
        }

        public async Task CreateAbonamentAsync(CreateAbonamentDto dto)
        {
            var abonament = new Abonament
            {
                Tip = dto.Tip,
                Pret = dto.Pret,
                ClientId = dto.ClientId,
                DataStart = DateTime.Now,
                DataExpirare = DateTime.Now.AddMonths(1)
            };

            _context.Abonamente.Add(abonament);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAbonamentAsync(int id)
        {
            var ab = await _context.Abonamente.FindAsync(id);
            if (ab == null) return false;

            _context.Abonamente.Remove(ab);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}