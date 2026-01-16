namespace FitHubAdmin.DTOs
{
    public class CreateClientDto
    {
        public string Nume { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    public class ClientResponseDto
    {
        public int Id { get; set; }
        public string Nume { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        // Nu mai avem int (0, 1, 2)
        // Avem status clar text
        public string StatusAbonament { get; set; } = "Inactiv"; 
        
        // Si tipul abonamentului curent (ex: Lunar)
        public string TipAbonament { get; set; } = "-";
    }
}