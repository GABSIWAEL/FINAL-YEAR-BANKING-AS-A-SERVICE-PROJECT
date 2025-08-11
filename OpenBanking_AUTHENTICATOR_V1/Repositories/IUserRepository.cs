using OpenBanking_AUTHENTICATOR_V1.Models;
using OpenBanking_AUTHENTICATOR_V1.Dtos;


namespace OpenBanking_AUTHENTICATOR_V1.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByGoogleIdAsync(string googleId);
        Task AddAsync(User user);
        Task UpdateAsync(User user);  // Add this
Task AssignPlanValidatorAsync(int userId, PlanValidator planValidator, string apiKey, DateTime apiKeyExpiry);
        Task<User?> GetByIdAsync(int userId);

    
    }
}
