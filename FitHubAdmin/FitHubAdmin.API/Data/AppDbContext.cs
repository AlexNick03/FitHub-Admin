using Microsoft.EntityFrameworkCore;

namespace FitHubAdmin.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}