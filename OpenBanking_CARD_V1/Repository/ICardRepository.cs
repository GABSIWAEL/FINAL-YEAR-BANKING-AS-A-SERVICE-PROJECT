using OpenBanking_CARD_V1.Models;
using System.Threading.Tasks;

namespace OpenBanking_CARD_V1.Repository
{
   
    public interface ICardRepository
    {
        Task<IEnumerable<Card>> GetAllCardsAsync();
    }
}