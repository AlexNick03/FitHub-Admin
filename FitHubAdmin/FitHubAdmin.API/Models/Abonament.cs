using System.Text.Json.Serialization;

namespace FitHubAdmin.Models
{
    public class Abonament
    {
        public int Id { get; set; }
        public string Tip { get; set; } = string.Empty; // Lunar, Anual
        public decimal Pret { get; set; }
        public DateTime DataStart { get; set; }
        public DateTime DataExpirare { get; set; }

        // RELATIA (Foreign Key) - Legatura cu Clientul
        public int ClientId { get; set; }

        [JsonIgnore] // Important: Sa nu intre in bucla infinita cand ceri datele
        public Client? Client { get; set; }
    }
}