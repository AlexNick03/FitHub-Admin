using FitHubAdmin.Models; 

namespace FitHubAdmin.DTOs
{
    //Ce asteaptă serverul pentru a crea un abonament 
    public class CreateAbonamentDto
    {
       
        public TipAbonament Tip { get; set; } 
        public DurataAbonament Durata { get; set; }
        
        public int ClientId { get; set; }
    }
    
    //Ce raspunde serverul cand is se cere lsita de abonaente
    public class AbonamentResponseDto
    {
        public int Id { get; set; }
        
       
        public TipAbonament Tip { get; set; } 
        public DurataAbonament Durata { get; set; }
        
        public decimal Pret { get; set; }
        public DateTime DataStart { get; set; }
        public DateTime DataExpirare { get; set; }
        public int ClientId { get; set; }
        public string NumeClient { get; set; } = string.Empty;
    }
}