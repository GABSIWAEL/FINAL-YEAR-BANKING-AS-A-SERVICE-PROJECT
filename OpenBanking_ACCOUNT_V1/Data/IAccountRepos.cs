using OpenBanking_ACCOUNT_V1.Models;

namespace OpenBanking_ACCOUNT_V1.Data
{
    public interface IAccountRepos
    {
        bool SaveChanges();

        IEnumerable<Account> GetAllPlatforms();
        Account? GetPlatformById(int id);
        void CreatePlatform(Account account);
    }
}
