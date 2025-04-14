using Microsoft.EntityFrameworkCore;
using OpenBanking_ATM_V1.Models;
using System.Collections.Generic;

namespace OpenBanking_ATM_V1.Data
{
    public class AppDbContext : DbContext // Fixed missing inheritance from DbContext
    {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Atm> Atm { get; set; }
}
}
