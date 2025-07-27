using OpenBanking_ACCOUNT_V1.Models;
using System.Threading.Tasks;

namespace OpenBanking_ACCOUNT_V1.Repository
{
    public interface IAccountRepository
    {
        Task<Account> GetAccountWithDetailsAsync(string bankId, string accountId);
        Task<List<Account>> GetAccountsInBank(string bankId);  
        Task<List<Account>> GetAccountBalancesByBankId(string bankId);  
        Task<Agent> GetAgentByBankIdAndAgentIdAsync(string bankId, string agent_id); 
        Task<List<Account>> GetAccountsHeld(string bankId); 
        Task<List<Agent>> GetAgentsAtBank(string bankId); 
        Task<List<Account>> GetFastFirehoseAccountsAtBank(string bankId); //this method didnt go well missing some info inide the balance owners 

        Task<Agent> GetAgent(string bankId, string agent_id); 
        
    }
}