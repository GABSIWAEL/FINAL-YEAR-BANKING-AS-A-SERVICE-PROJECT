using Microsoft.EntityFrameworkCore;
using OpenBanking_AUTHENTICATOR_V1.Data;
using OpenBanking_AUTHENTICATOR_V1.Models;

namespace OpenBanking_AUTHENTICATOR_V1.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByGoogleIdAsync(string googleId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.GoogleId == googleId);
        }

        public async Task AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
    }
}
