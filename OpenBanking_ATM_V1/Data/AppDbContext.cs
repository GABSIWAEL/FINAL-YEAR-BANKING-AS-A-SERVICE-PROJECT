using Microsoft.EntityFrameworkCore;
using OpenBanking_ATM_V1.Dtos;
using OpenBanking_ATM_V1.Models;
using System.Collections.Generic;

namespace OpenBanking_ATM_V1.Data
{
    public class AppDbContext : DbContext // Fixed missing inheritance from DbContext
    {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

        public DbSet<ATMAttributes> ATMAttributes { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Meta> Metas { get; set; }
        public DbSet <Atm> Atms { get; set; }

    }
}
