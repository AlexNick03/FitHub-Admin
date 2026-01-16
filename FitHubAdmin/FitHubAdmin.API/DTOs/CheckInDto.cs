namespace FitHubAdmin.DTOs
{
    public class CheckInDto
    {
        public int ClientId { get; set; }
    }
    
    public class CheckInResponseDto
    {
        public string NumeClient { get; set; }
        public string StatusAcces { get; set; } // "Permis" sau "Respins"
        public string Mesaj { get; set; }       // "Abonament valid" sau "Expirat la data..."
        public DateTime DataOra { get; set; }
    }
}