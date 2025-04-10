
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
    }
}
