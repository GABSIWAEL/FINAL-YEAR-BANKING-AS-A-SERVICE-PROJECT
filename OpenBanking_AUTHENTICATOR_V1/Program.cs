using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OpenBanking_AUTHENTICATOR_V1.Data;
using OpenBanking_AUTHENTICATOR_V1.Repositories;
using Prometheus;
using System.Text;
using System.Text.Json.Serialization;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// 👉 DATABASE
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// 👉 REPOSITORY
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddHttpClient<KongService>();
// 👉 DATA PROTECTION
builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo("/root/.aspnet/DataProtection-Keys"))
    .SetApplicationName("OpenBankingAuthenticator");

// 👉 SESSION CACHE (memory for now)
builder.Services.AddDistributedMemoryCache();

// 👉 SESSION CONFIGURATION
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".OpenBanking.Auth";
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

// 👉 AUTHENTICATION
builder.Services.AddAuthentication(options =>
{
    // Default authentication scheme
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.Cookie.Name = ".AspNetCore.Cookies";
    options.Cookie.HttpOnly = true;
    options.Cookie.SameSite = SameSiteMode.None;  // Must be None for OAuth
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Ensure HTTPS in production
})
.AddGoogle(options =>
{
    options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = true;
    options.SaveToken = true;

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))
    };
});

// 👉 CONTROLLERS & JSON OPTIONS
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });

// 👉 SWAGGER DOCS
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "OpenBanking_AUTHENTICATOR_V1",
        Version = "v1",
        Description = "Google OAuth Authentication API"
    });
});

// 👉 KESTREL DOCKER PORT
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8090); // Matches docker-compose exposed port
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins("http://localhost:8083") // Your Angular app URL
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();  // Allow cookies to be sent with requests
    });
});

// 👉 BUILD APP
var app = builder.Build();
app.UseCors("AllowSpecificOrigin");  // Apply the CORS policy


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

// ✅ MIDDLEWARE ORDER MATTERS
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "AUTH API V1");
    c.RoutePrefix = string.Empty;
});

// IMPORTANT: Must come BEFORE session + auth
app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.None,
    Secure = CookieSecurePolicy.None // Must match cookie settings for HTTP local dev
});

app.UseRouting();
app.UseSession();        // Required before authentication
app.UseAuthentication();
app.UseAuthorization();

app.UseHttpMetrics();    // Prometheus metrics
app.MapControllers();
app.MapMetrics();        // Exposes /metrics

app.Run();
