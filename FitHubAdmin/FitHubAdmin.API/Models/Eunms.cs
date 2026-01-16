using System.Text.Json.Serialization;

namespace FitHubAdmin.Models
{
    // Convertoarele astea fac ca in Swagger sa apara text ("Lunar"), nu cifre (0)
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TipAbonament
    {
        Bronze,
        Silver,
        Gold
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DurataAbonament
    {
        Lunar,
        Anual
    }
}