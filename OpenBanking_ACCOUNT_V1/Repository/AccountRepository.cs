// Repository/AccountRepository.cs
using Microsoft.EntityFrameworkCore;
using OpenBanking_ACCOUNT_V1.Data;
using OpenBanking_ACCOUNT_V1.Models;
using System.Threading.Tasks;
using OpenBanking_ACCOUNT_V1.Dtos;


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
                .Include(a => a.balances)
                .Include(a => a.views_available)
                .Include(a => a.account_routings)
                .Include(a => a.account_Attributes)
                .Include(a => a.tags)
                .FirstOrDefaultAsync(a => a.id == accountId && a.Bank_id == bankId);
        }

        //***** *********************************************************************************************
        public async Task<List<Account>> GetAccountsInBank(string bankId)
        {
          return await _context.Accounts
            .Include(a => a.views_available)
            .Where(a => a.Bank_id == bankId)
            .ToListAsync();
        }

     public async Task<List<Account>> GetAccountBalancesByBankId(string bankId)
        {
          return await _context.Accounts
            .Include(a => a.balances)
            .Include(a => a.account_routings)
            .Where(a => a.Bank_id == bankId)
            .ToListAsync();
        }


    public async Task<Agent> GetAgentByBankIdAndAgentIdAsync(string bankId, string agent_id)
{
    bankId = bankId.ToLower();
    agent_id = agent_id.ToLower();
    return await _context.agents
                         .FirstOrDefaultAsync(a => a.Bank_id.ToLower() == bankId && a.agent_id.ToLower() == agent_id);
}



    public async Task<List<Account>> GetAccountsHeld(string bankId)
        {
          return await _context.Accounts
            .Include(a => a.account_routings)
            .Where(a => a.Bank_id == bankId)
            .ToListAsync();
         }

    public async Task<List<Agent>> GetAgentsAtBank(string bankId)
        {
          return await _context.agents
            .Where(a => a.Bank_id == bankId)
            .ToListAsync();
         }
        public async Task<Agent> GetAgent(string bankId, string agent_id)
        {
            return await _context.agents
                .FirstOrDefaultAsync(a => a.Bank_id == bankId && a.agent_id == agent_id);
        }


                  public async Task<List<Account>> GetFastFirehoseAccountsAtBank(string bankId)
                {
                    return await _context.Accounts
                        .Include(a => a.owners)
                        .Include(a => a.balances)
                        .Include(a => a.account_routings)
                        .Include(a => a.account_Attributes)
                        .Where(a => a.Bank_id == bankId)
                        .ToListAsync();
                }

        public async Task<IEnumerable<GrpcAccountModelForCardOfCurrentUser>> GetAccountsByIdsAsync(IEnumerable<string> accountIds)
        {
            return await _context.Accounts
                .Include(a => a.views_available)
                .Where(a => accountIds.Contains(a.id))
                .Select(a => new GrpcAccountModelForCardOfCurrentUser
                {
                    id = a.id,
                    label = a.label,
                    Bank_id = a.Bank_id,
                    views_available = a.views_available.ToList()
                })
                .ToListAsync();
        }
        public async Task<Account> CreateAccountAsync(string bankId, CreateAccountDto createAccountDto)
{
    var account = new Account
    {
        id = Guid.NewGuid().ToString(),
        Bank_id = bankId,
        user_id = createAccountDto.user_id,
        label = createAccountDto.label,
        product_code = createAccountDto.product_code,
        branch_id = createAccountDto.branch_id,
        balances = createAccountDto.balances,
        account_routings = createAccountDto.account_routings
    };

    _context.Accounts.Add(account);
    await _context.SaveChangesAsync();

    return account;
}

public async Task<Account> GetFullAccountByIdAsync(string bankId, string accountId)
{
    return await _context.Accounts
        .Include(a => a.balances)
        .Include(a => a.account_routings)
        .Include(a => a.account_Attributes)
        .FirstOrDefaultAsync(a => a.id == accountId && a.Bank_id == bankId);
}


    }
}
