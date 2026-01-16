using FitHubAdmin.Models;
using Microsoft.EntityFrameworkCore;

namespace FitHubAdmin.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Client> Clienti { get; set; }
        
        // Tabela pentru Abonamente
        public DbSet<Abonament> Abonamente { get; set; }
    }
}