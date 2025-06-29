
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

    }   
}
