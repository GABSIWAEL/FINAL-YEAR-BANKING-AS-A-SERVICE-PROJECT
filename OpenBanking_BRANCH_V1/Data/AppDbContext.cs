using OpenBanking_BRANCH_V1.Models;
using Microsoft.EntityFrameworkCore;
namespace OpenBanking_BRANCH_V1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Branch> Card { get; set; }
    }
}
