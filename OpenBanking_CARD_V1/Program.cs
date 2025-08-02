using OpenBanking_CARD_V1.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Grpc.AspNetCore.Server;
using AccountService;
using OpenBanking_CARD_V1.Repository;
using OpenBanking_CARD_V1.SyncDataService.Grpc; // ✅ Needed for AccountGrpcService

var builder = WebApplication.CreateBuilder(args);

// 👉 Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 👉 PostgreSQL DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// 👉 Repositories
builder.Services.AddScoped<ICardRepository, CardRepository>();

// 👉 gRPC server (if you host gRPC endpoints here)
builder.Services.AddGrpc();

// 👉 gRPC client for Account microservice
builder.Services.AddGrpcClient<GrpcAccount.GrpcAccountClient>(o =>
{
    o.Address = new Uri("http://account-service:8088"); // Use service name in Docker
});

// ✅ Register your AccountGrpcService for DI
builder.Services.AddScoped<AccountGrpcService>();

// 👉 HTTP port setup (Docker binding)
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8086);
});
AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

var app = builder.Build();

// 👉 Run migrations automatically on startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

// 👉 Configure middleware
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API CARD V1");
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
