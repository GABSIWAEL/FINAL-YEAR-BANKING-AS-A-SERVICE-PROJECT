using Microsoft.EntityFrameworkCore;
using OpenBanking_ACCOUNT_V1.Models;

namespace OpenBanking_ACCOUNT_V1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Owners> Owners { get; set; }
        public DbSet<Tags> Tags { get; set; }
        public DbSet<Account_attributes> AccountAttributes { get; set; }
        public DbSet<Account_routings> AccountRoutings { get; set; }
        public DbSet<Views_available> ViewsAvailable { get; set; }
        public DbSet<Agent> agents { get; set; }

        // ✅ Put this INSIDE the class
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Balance>()
                .HasOne(b => b.Account)
                .WithMany(a => a.balances)
                .HasForeignKey(b => b.Accountid);

            modelBuilder.Entity<Owners>()
                .HasOne(o => o.Account)
                .WithMany(a => a.owners)
                .HasForeignKey(o => o.Accountid);

            modelBuilder.Entity<Account_routings>()
                .HasOne(r => r.Account)
                .WithMany(a => a.account_routings)
                .HasForeignKey(r => r.Accountid);

            modelBuilder.Entity<Account_attributes>()
                .HasOne(aa => aa.Account)
                .WithMany(a => a.account_Attributes)
                .HasForeignKey(aa => aa.Accountid);

            base.OnModelCreating(modelBuilder);
        }
    }
}
