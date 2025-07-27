using Microsoft.EntityFrameworkCore;
using OpenBanking_ACCOUNT_V1.Data;
using OpenBanking_ACCOUNT_V1.Repository;
using OpenTelemetry;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Linq;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(); // If you use UnitOfWork
builder.Services.AddAutoMapper(typeof(OpenBanking_ACCOUNT_V1.Helpers.AutoMapperProfile));
// Configure EF Core with PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
// ✅ Add gRPC
builder.Services.AddGrpc();
// Configure controllers with System.Text.Json, ignoring cycles and indenting output
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations(); // optional for [SwaggerOperation]
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "OpenBanking_ACCOUNT_V1",
        Version = "v1",
        Description = "API for managing open banking accounts"
    });
});

// Serilog logging configuration
configureLogging();
builder.Host.UseSerilog();

// Kestrel configuration
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8088);
});

// OpenTelemetry configuration
builder.Services.AddOpenTelemetry()
    .ConfigureResource(res => res.AddService("OpenBanking_ACCOUNT_V1"))
    .WithMetrics(m =>
    {
        m.AddAspNetCoreInstrumentation()
         .AddHttpClientInstrumentation()
         .AddPrometheusExporter(); // 👈 Add Prometheus exporter
    })
    .WithTracing(t =>
    {
        t.AddAspNetCoreInstrumentation()
         .AddHttpClientInstrumentation()
         .AddEntityFrameworkCoreInstrumentation();
        t.AddOtlpExporter(opt =>
        {
            opt.Endpoint = new Uri("http://dashboard_ACCOUNT:18889");
        });
    });

// OpenTelemetry logging
builder.Logging.AddOpenTelemetry(opt =>
{
    opt.AddConsoleExporter()
       .SetResourceBuilder(ResourceBuilder.CreateDefault()
           .AddService("OpenBanking_ACCOUNT_V1"));
    opt.AddOtlpExporter(x =>
    {
        x.Endpoint = new Uri("http://dashboard_ACCOUNT:18889");
    });
});

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API ACCOUNT V1");
    c.RoutePrefix = string.Empty; // Swagger at root: http://localhost:8088/
});
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// OpenTelemetry Prometheus scraping endpoint
app.UseOpenTelemetryPrometheusScrapingEndpoint(); // 👈 Add this line

app.Run();

void configureLogging()
{
    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

    var configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{environment}.json", optional: true)
        .Build();

    try
    {
        Log.Logger = new LoggerConfiguration()
        .Enrich.FromLogContext()
        .Enrich.WithExceptionDetails()
        .Enrich.WithProperty("Environment", environment)
        .WriteTo.Debug()
        .WriteTo.Console()
        .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
        {
            AutoRegisterTemplate = true,
            IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-{environment.ToLower()}-{DateTime.UtcNow:yyyy-MM}",
            NumberOfReplicas = 1,
            NumberOfShards = 2,
            CustomFormatter = new Serilog.Formatting.Compact.RenderedCompactJsonFormatter()
        })
        .ReadFrom.Configuration(configuration)
        .CreateLogger();

        Log.Information("✅ Serilog + Elasticsearch initialized.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Failed to initialize Elasticsearch sink: {ex.Message}");
        Console.WriteLine(ex);
    }
}

ElasticsearchSinkOptions ConfigureElasticSink(IConfigurationRoot configuration, string environment)
{
    return new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
    {
        AutoRegisterTemplate = true,
        IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-{environment.ToLower()}-{DateTime.UtcNow:yyyy-MM}",
        NumberOfReplicas = 1,
        NumberOfShards = 2
    };
}
Log.Information("🔥 Application started - this should be visible in Elasticsearch");
