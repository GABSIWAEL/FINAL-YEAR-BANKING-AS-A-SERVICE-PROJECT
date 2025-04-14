using Microsoft.EntityFrameworkCore;
using OpenBanking_CARD_V1.Models;
using System.Collections.Generic;

namespace OpenBanking_CARD_V1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Card> Card { get; set; }
    }
}
