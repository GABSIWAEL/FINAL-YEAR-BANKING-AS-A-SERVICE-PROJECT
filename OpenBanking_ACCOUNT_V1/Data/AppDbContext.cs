using Microsoft.EntityFrameworkCore;
using OpenBanking_ACCOUNT_V1.Models;

namespace OpenBanking_ACCOUNT_V1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt) { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Owners> Owners { get; set; }
        public DbSet<Tags> Tags { get; set; }
        public DbSet<Balance> Balances { get; set; }                 // ✅ added
        public DbSet<Account_attributes> AccountAttributes { get; set; }
        public DbSet<Account_routings> AccountRoutings { get; set; }
        public DbSet<Views_available> ViewsAvailable { get; set; }
        public DbSet<Agent> Agents { get; set; }                     // ✅ PascalCase

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Account 1 - * Balances
            modelBuilder.Entity<Balance>()
                .HasOne(b => b.Account)
                .WithMany(a => a.balances)
                .HasForeignKey(b => b.Accountid)
                .OnDelete(DeleteBehavior.Cascade);

            // Account 1 - * Owners
            modelBuilder.Entity<Owners>()
                .HasOne(o => o.Account)
                .WithMany(a => a.owners)
                .HasForeignKey(o => o.Accountid)
                .OnDelete(DeleteBehavior.Cascade);

            // Account 1 - * Account_routings
            modelBuilder.Entity<Account_routings>()
                .HasOne(r => r.Account)
                .WithMany(a => a.account_routings)
                .HasForeignKey(r => r.Accountid)
                .OnDelete(DeleteBehavior.Cascade);

            // Account 1 - * Account_attributes
            modelBuilder.Entity<Account_attributes>()
                .HasOne(aa => aa.Account)
                .WithMany(a => a.account_Attributes)
                .HasForeignKey(aa => aa.Accountid)
                .OnDelete(DeleteBehavior.Cascade);

            // ✅ Account 1 - * Views_available
            modelBuilder.Entity<Views_available>()
                .HasOne(v => v.Account)
                .WithMany(a => a.views_available)
                .HasForeignKey(v => v.Accountid)
                .OnDelete(DeleteBehavior.Cascade);

            // ✅ Account 1 - * Tags
            modelBuilder.Entity<Tags>()
                .HasOne(t => t.Account)
                .WithMany(a => a.tags)
                .HasForeignKey(t => t.Accountid)
                .OnDelete(DeleteBehavior.Cascade);

           
            // (Optional) store enums as strings for readability
            modelBuilder.Entity<Account_routings>()
                .Property(r => r.Scheme).HasConversion<string>();
            modelBuilder.Entity<Account_attributes>()
                .Property(a => a.type).HasConversion<string>();
            modelBuilder.Entity<Balance>()
                .Property(b => b.currency).HasConversion<string>();
            modelBuilder.Entity<Agent>()
                .Property(a => a.currency).HasConversion<string>();
            modelBuilder.Entity<Views_available>()
                .Property(v => v.alias).HasConversion<string>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
