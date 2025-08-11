using Microsoft.EntityFrameworkCore;
using OpenBanking_AUTHENTICATOR_V1.Data;
using OpenBanking_AUTHENTICATOR_V1.Models;
using OpenBanking_AUTHENTICATOR_V1.Dtos;

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
        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    
    
    
public async Task AssignPlanValidatorAsync(int userId, PlanValidator planValidator, string apiKey, DateTime apiKeyExpiry)
{
    if (!Enum.IsDefined(typeof(BuisnessPlan), planValidator.buisnessPlan))
        throw new ArgumentException($"Invalid BuisnessPlan value: {planValidator.buisnessPlan}");

    var user = await _context.Users.FindAsync(userId);
    if (user == null)
        throw new KeyNotFoundException($"User with Id {userId} not found.");

    user.buisnessPlan = planValidator.buisnessPlan;
    user.ApiKey = apiKey;
    user.ApiKeyExpiry = apiKeyExpiry;

    _context.Users.Update(user);
    await _context.SaveChangesAsync();
}




    public async Task<User?> GetByIdAsync(int userId)
{
    return await _context.Users.FindAsync(userId);
}

    
    }


}
