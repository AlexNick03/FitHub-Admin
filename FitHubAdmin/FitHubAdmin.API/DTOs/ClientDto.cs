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
        public int NumarAbonamente { get; set; } // Va fi 0 momentan
    }
}