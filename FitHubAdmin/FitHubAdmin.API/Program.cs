using FitHubAdmin.Data;
using FitHubAdmin.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
    
builder.Services.AddScoped<ClientService>();
builder.Services.AddScoped<AbonamentService>();
builder.Services.AddScoped<AccesService>();
builder.Services.AddControllers().AddJsonOptions(options =>
    {
        
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    })
    .ConfigureApiBehaviorOptions(options =>
    {
        
        options.InvalidModelStateResponseFactory = context =>
        {
           
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
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        context.Database.Migrate(); // Asta aplica migrarile si creeaza baza daca nu exista
    }
    catch (Exception ex)
    {
        Console.WriteLine("Eroare la migrarea bazei de date: " + ex.Message);
    }
}
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "FitHub Admin API v1");
        c.RoutePrefix = "swagger";
    });
    app.MapGet("/", context => {
        context.Response.Redirect("/swagger/index.html");
        return Task.CompletedTask;
    });
}


//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();