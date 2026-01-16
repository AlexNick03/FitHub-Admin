namespace FitHubAdmin.Models
{
    public class IstoricAcces
    {
        public int Id { get; set; }
        public DateTime DataAcces { get; set; } // Cand a intrat

        // Legatura cu Clientul
        public int ClientId { get; set; }
        public Client? Client { get; set; }
    }
}