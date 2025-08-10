using OpenBanking_NOTIFICATION_V1.Shared.Services;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHostedService<AccountCreatedConsumer>();
builder.Services.AddHostedService<AccountAttributeCreatedConsumer>();
builder.Services.AddHostedService<AtmCreatedConsumer>();
builder.Services.AddHostedService<AtmAttributeCreatedConsumer>();

builder.Services.AddSingleton<EmailSenderService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseRouting();
app.UseHttpMetrics(); // <-- collect ASP.NET Core HTTP metrics

app.UseEndpoints(endpoints =>
{
    endpoints.MapMetrics(); // <-- expose /metrics for Prometheus to scrape
});

app.Run();
