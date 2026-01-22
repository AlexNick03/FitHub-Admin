
public class IstoricAccesResponseDto
{   //Raspunsul serverului cand i se cere istoricul de acces 
    public int Id { get; set; }
    public string NumeClient { get; set; } = string.Empty;
    public DateTime DataAcces { get; set; }
}