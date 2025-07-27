using Microsoft.EntityFrameworkCore;
using OpenBanking_AUTHENTICATOR_V1.Models;

namespace OpenBanking_AUTHENTICATOR_V1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) {}

        public DbSet<User> Users { get; set; } = null!;
    }
}
