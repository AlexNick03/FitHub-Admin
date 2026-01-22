namespace FitHubAdmin.DTOs
{   //Ce asteapta serverul atunci cand se creeaza un client nou
    public class CreateClientDto
    {
        public string Nume { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
    //Ce raspunde serverul cand i se cere lista cu clienti
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