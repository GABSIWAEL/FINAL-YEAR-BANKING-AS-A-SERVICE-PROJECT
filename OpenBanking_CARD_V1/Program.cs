using OpenBanking_CARD_V1.Data;
using Microsoft.EntityFrameworkCore; // Ajout de l'instruction using manquante pour AddDbContext
using Microsoft.Extensions.DependencyInjection; // Ajout de l'instruction using pour les extensions de services Entity Framework Core

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8086);
});
var app = builder.Build();

// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API CARD V1");
    c.RoutePrefix = string.Empty; // Swagger will be at root e.g., http://localhost:8088/
});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
