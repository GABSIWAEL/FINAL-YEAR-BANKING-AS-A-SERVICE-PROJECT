using Microsoft.EntityFrameworkCore;
using OpenBanking_ACCOUNT_V1.Data;
using OpenBanking_ACCOUNT_V1.Repository;
using OpenTelemetry;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;

using System.Reflection;
using System.Text.Json.Serialization;
using OpenBanking_ACCOUNT_V1.SyncDataService.Grpc;
using Microsoft.AspNetCore.Server.Kestrel.Core;


var builder = WebApplication.CreateBuilder(args);

// ✅ Repositories and AutoMapper
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddAutoMapper(typeof(OpenBanking_ACCOUNT_V1.Helpers.AutoMapperProfile));

// ✅ EF Core with PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ gRPC
builder.Services.AddGrpc();

// ✅ JSON config
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});

// ✅ Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "OpenBanking_ACCOUNT_V1",
        Version = "v1",
        Description = "API for managing open banking accounts"
    });
});

// ✅ Serilog
configureLogging();
builder.Host.UseSerilog();

// ✅ Kestrel
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8088, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http2; // << This is critical
    });
});


// ✅ OpenTelemetry
builder.Services.AddOpenTelemetry()
    .ConfigureResource(res => res.AddService("OpenBanking_ACCOUNT_V1"))
    .WithMetrics(m =>
    {
        m.AddAspNetCoreInstrumentation()
         .AddHttpClientInstrumentation()
         .AddPrometheusExporter();
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

// ✅ Apply DB Migrations
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

// ✅ Swagger UI
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API ACCOUNT V1");
    c.RoutePrefix = string.Empty;
});

// Optional: Enable if HTTPS is configured
// app.UseHttpsRedirection();

app.UseAuthorization();

// ✅ Routes
app.MapGrpcService<GrpcAccountService>();
app.MapControllers();

app.MapGet("/Protos/Accounts.proto", async context =>
{
    var env = app.Services.GetRequiredService<IWebHostEnvironment>();
    var protoPath = Path.Combine(env.ContentRootPath, "Protos", "Accounts.proto");
    var protoContent = await File.ReadAllTextAsync(protoPath);
    await context.Response.WriteAsync(protoContent);
});


// ✅ Prometheus metrics endpoint
app.UseOpenTelemetryPrometheusScrapingEndpoint();

// ✅ Run app
app.Run();


// --------------------
// 🔧 Logging Setup
// --------------------
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
