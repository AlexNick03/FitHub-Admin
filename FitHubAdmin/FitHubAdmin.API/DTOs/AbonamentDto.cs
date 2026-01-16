using FitHubAdmin.Models; // Ca sa recunoasca Enums

namespace FitHubAdmin.DTOs
{
    public class CreateAbonamentDto
    {
        // Swagger va face automat un Dropdown list pentru astea!
        public TipAbonament Tip { get; set; } 
        public DurataAbonament Durata { get; set; }
        
        public int ClientId { get; set; }
    }

    public class AbonamentResponseDto
    {
        public int Id { get; set; }
        
        // Le punem ca string in raspuns ca sa fie citibile
        public TipAbonament Tip { get; set; } 
        public DurataAbonament Durata { get; set; }
        
        public decimal Pret { get; set; }
        public DateTime DataStart { get; set; }
        public DateTime DataExpirare { get; set; }
        public int ClientId { get; set; }
        public string NumeClient { get; set; } = string.Empty;
    }
}