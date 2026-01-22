namespace FitHubAdmin.DTOs
{
    // Ce trimite receptionerul (Doar ID-ul)
    public class CheckInDto
    {
        public int ClientId { get; set; }
    }

    // Ce raspunde serverul cand faci Check-In
    public class CheckInResponseDto
    {
        public string NumeClient { get; set; }
        public string StatusAcces { get; set; } // "PERMIS" sau "RESPINS"
        public string Mesaj { get; set; }
        public DateTime DataOra { get; set; }
    }

    // Ce raspunde serverul cand ceri lista de istoric 
    public class IstoricAccesResponseDto
    {
        public int Id { get; set; }
        public string NumeClient { get; set; } = string.Empty;
        public DateTime DataAcces { get; set; }
    }
}