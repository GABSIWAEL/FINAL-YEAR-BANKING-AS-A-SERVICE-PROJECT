// Repository/AccountRepository.cs
using Microsoft.EntityFrameworkCore;
using OpenBanking_ACCOUNT_V1.Data;
using OpenBanking_ACCOUNT_V1.Models;
using System.Threading.Tasks;

namespace OpenBanking_ACCOUNT_V1.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _context;

        public AccountRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Account> GetAccountWithDetailsAsync(string bankId, string accountId)
        {
            return await _context.Accounts
                .Include(a => a.owners)
                .Include(a => a.balance)
                .Include(a => a.views_available)
                .Include(a => a.account_routings)
                .Include(a => a.account_Attributes)
                .Include(a => a.tags)
                .FirstOrDefaultAsync(a => a.id == accountId && a.Bank_id == bankId);
        }
    }
}
