using Microsoft.EntityFrameworkCore;
using OpenBanking_ACCOUNT_V1.Data;
using OpenBanking_ACCOUNT_V1.Repository;
using OpenTelemetry;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.Collections.Generic; // Imports generic collections like List, Dictionary, etc.
using System.Linq;                 // Imports LINQ extension methods for querying collections.
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(); // if you're using Unit of Work
builder.Services.AddAutoMapper(typeof(OpenBanking_ACCOUNT_V1.Helpers.AutoMapperProfile));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8088);
});
builder.Services.AddOpenTelemetry()
    .ConfigureResource(res => res.AddService("OpenBanking_ACCOUNT_V1"))
    .WithMetrics(m =>
    {
        m.AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation();
        m.AddOtlpExporter(opt =>
        {
            opt.Endpoint = new Uri("http://account.dashboard:18889");
        });

    }).WithTracing(t =>
    {
        t.AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddEntityFrameworkCoreInstrumentation();
        t.AddOtlpExporter(opt =>
        {
            opt.Endpoint = new Uri("http://account.dashboard:18889");
        });
    });
builder.Logging.AddOpenTelemetry(opt =>
{
    opt.AddConsoleExporter()
    .SetResourceBuilder(ResourceBuilder.CreateDefault()
        .AddService("OpenBanking_ACCOUNT_V1"));
    opt.AddOtlpExporter(x =>
    {
        x.Endpoint = new Uri("http://account.dashboard:18889");
    });
});
    var app = builder.Build();

// Apply migrations on startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}
// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API ACCOUNT V1");
    c.RoutePrefix = string.Empty; // Swagger will be at root e.g., http://localhost:8088/
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

