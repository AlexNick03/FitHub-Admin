using FitHubAdmin.Data;
using FitHubAdmin.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=fithub.db"));

builder.Services.AddScoped<ClientService>();
builder.Services.AddScoped<AbonamentService>();
builder.Services.AddControllers();
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