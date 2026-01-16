namespace FitHubAdmin.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Nume { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime DataInregistrare { get; set; }

        //Actualizare model existent
        public ICollection<Abonament> Abonamente { get; set; } = new List<Abonament>();
        
    }
}