using AutoMapper;
using Microsoft.OpenApi.Models;
using OpenBanking_ATM_V1.Repository;
using OpenBanking_ATM_V1.Shared.Services;

var builder = WebApplication.CreateBuilder(args);

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Register your ATM repository (singleton)
builder.Services.AddSingleton<IAtmRepository>(sp =>
{
    var mapper = sp.GetRequiredService<IMapper>();
    var mongoConn = builder.Configuration.GetConnectionString("DefaultConnection")
                   ?? throw new InvalidOperationException("DefaultConnection is missing in configuration.");
    var mongoDbName = "atmdb";
    return new AtmRepository(mapper, mongoConn, mongoDbName);
});
builder.Services.AddSingleton<RabbitMqPublisher>();
// Add controllers
builder.Services.AddControllers();

// Add Swagger/OpenAPI generation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ATM API",
        Version = "v1",
        Description = "API for managing open banking ATMs"
    });

    // Enable annotations if you’re using [SwaggerOperation] etc.
    c.EnableAnnotations();

    // If using XML comments for documentation:
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});


// Configure Kestrel to listen on HTTP port 8082
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8082);
});

var app = builder.Build();

// Enable Swagger middleware before routing
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ATM API V1");
    c.RoutePrefix = string.Empty;
});


// Configure routing and authorization
app.UseRouting();
app.UseAuthorization();

// Map controller endpoints
app.MapControllers();

app.Run();
