using FitHubAdmin.Data;
using FitHubAdmin.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=fithub.db"));

builder.Services.AddScoped<ClientService>();
builder.Services.AddScoped<AbonamentService>();
builder.Services.AddControllers().AddJsonOptions(options =>
    {
        // Asta pastreaza functionalitatea de a scrie "Lunar" in loc de cifre
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    })
    .ConfigureApiBehaviorOptions(options =>
    {
        // AICI E SMECHERIA PENTRU MESAJ PERSONALIZAT
        options.InvalidModelStateResponseFactory = context =>
        {
            // Cand apare o eroare de validare (ex: ai scris "Lunarr"), intram aici:
            return new BadRequestObjectResult(new
            {
                Eroare = "Date invalide!",
                Mesaj = "Ai introdus o valoare gresita pentru Tip sau Durata. Verifica daca ai scris corect (ex: Bronze, Silver, Gold / Lunar, Anual).",
                Detalii = "Verifica daca ai greseli de tastare."
            });
        };
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "FitHub Admin API v1");
    c.RoutePrefix = "swagger";
});

//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();