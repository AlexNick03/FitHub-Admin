namespace FitHubAdmin.DTOs
{
    public class CreateAbonamentDto
    {
        public string Tip { get; set; } = "Lunar";
        public decimal Pret { get; set; }
        public int ClientId { get; set; }
    }

    public class AbonamentResponseDto
    {
        public int Id { get; set; }
        public string Tip { get; set; } = string.Empty;
        public decimal Pret { get; set; }
        public DateTime DataStart { get; set; }
        public DateTime DataExpirare { get; set; }

        // AICI: Trimitem si ID-ul si Numele pentru UI
        public int ClientId { get; set; }
        public string NumeClient { get; set; } = string.Empty;
    }
}