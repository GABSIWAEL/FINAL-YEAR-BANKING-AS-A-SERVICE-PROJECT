using OpenBanking_AUTHENTICATOR_V1.Models;

namespace OpenBanking_AUTHENTICATOR_V1.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByGoogleIdAsync(string googleId);
        Task AddAsync(User user);
    }
}
