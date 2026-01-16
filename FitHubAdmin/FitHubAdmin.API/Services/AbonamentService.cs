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
                .Include(a => a.Client)
                .Select(a => new AbonamentResponseDto
                {
                    Id = a.Id,
                    Tip = a.Tip,       // Convertim Enum la text
                    Durata = a.Durata, // Convertim Enum la text
                    Pret = a.Pret,
                    DataStart = a.DataStart,
                    DataExpirare = a.DataExpirare,
                    ClientId = a.ClientId,
                    NumeClient = a.Client != null ? a.Client.Nume : "Client Sters"
                })
                .ToListAsync();
        }

        public async Task CreateAbonamentAsync(CreateAbonamentDto dto)
        {
            // A. Validare Suprapunere (codul vechi)
            bool areAbonamentActiv = await _context.Abonamente
                .AnyAsync(a => a.ClientId == dto.ClientId && a.DataExpirare > DateTime.Now);

            if (areAbonamentActiv)
            {
                throw new InvalidOperationException("Acest client are deja un abonament activ!");
            }

            // B. CALCUL PRET (Matricea ta de preturi)
            decimal pretCalculat = 0;

            if (dto.Durata == DurataAbonament.Lunar)
            {
                switch (dto.Tip)
                {
                    case TipAbonament.Bronze: pretCalculat = 140; break;
                    case TipAbonament.Silver: pretCalculat = 180; break;
                    case TipAbonament.Gold:   pretCalculat = 250; break;
                }
            }
            else // Anual
            {
                switch (dto.Tip)
                {
                    case TipAbonament.Bronze: pretCalculat = 1000; break;
                    case TipAbonament.Silver: pretCalculat = 1150; break;
                    case TipAbonament.Gold:   pretCalculat = 1225; break;
                }
            }

            // C. CALCUL DATA EXPIRARE (Demo vs Real)
            DateTime dataExpirare;
        
            if (dto.Durata == DurataAbonament.Lunar)
            {
                // LUNAR = 2 minute (PENTRU DEMO)
                dataExpirare = DateTime.Now.AddMinutes(2);
            }
            else
            {
                // ANUAL = 1 an (REAL)
                dataExpirare = DateTime.Now.AddYears(1);
            }

            var abonament = new Abonament
            {
                Tip = dto.Tip,
                Durata = dto.Durata,
                Pret = pretCalculat, // <--- Pretul vine din calcul, nu de la user
                ClientId = dto.ClientId,
                DataStart = DateTime.Now,
                DataExpirare = dataExpirare
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