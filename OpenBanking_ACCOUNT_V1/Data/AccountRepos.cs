using OpenBanking_ACCOUNT_V1.Models;
using System.Collections.Generic;
using System.Linq;
namespace OpenBanking_ACCOUNT_V1.Data
{
    public class AccountRepos : IAccountRepos
    {
        private readonly AppDbContext _context;

        public AccountRepos(AppDbContext context)
        {
            _context = context;
        }

        public void CreatePlatform(Account account)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }
            _context.Accounts.Add(account);
        }

        public IEnumerable<Account> GetAllPlatforms()
        {
            return _context.Accounts.ToList();
        }

        public Account? GetPlatformById(int id)
        {
            return _context.Accounts.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
