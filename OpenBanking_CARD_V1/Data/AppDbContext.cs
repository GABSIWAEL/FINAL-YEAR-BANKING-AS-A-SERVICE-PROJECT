using Microsoft.EntityFrameworkCore;
using OpenBanking_CARD_V1.Models;

namespace OpenBanking_CARD_V1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Card> Cards { get; set; }
        public DbSet<Replacement> Replacements { get; set; }
        public DbSet<Pin_reset> PinResets { get; set; }
        public DbSet<Card_attributes> CardAttributes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Card>()
                .HasKey(c => c.CardId);

            modelBuilder.Entity<Card>()
                .HasOne(c => c.Replacement)
                .WithOne(r => r.Card)
                .HasForeignKey<Replacement>(r => r.CardId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Card>()
                .HasMany(c => c.PinResets)
                .WithOne(p => p.Card)
                .HasForeignKey(p => p.CardId)
                .OnDelete(DeleteBehavior.Cascade);

           modelBuilder.Entity<Card>()
                .HasOne(c => c.CardAttributes)
                .WithOne(a => a.Card)
                .HasForeignKey<Card_attributes>(a => a.CardId);

        }
    }
}
