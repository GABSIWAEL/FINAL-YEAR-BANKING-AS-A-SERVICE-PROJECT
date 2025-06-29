using OpenBanking_ACCOUNT_V1.Models;
using System.Threading.Tasks;

namespace OpenBanking_ACCOUNT_V1.Repository
{
    public interface IAccountRepository
    {
        Task<Account> GetAccountWithDetailsAsync(string bankId, string accountId);
    }
}