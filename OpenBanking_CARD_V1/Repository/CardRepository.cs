// Repository/AccountRepository.cs
using Microsoft.EntityFrameworkCore;
using OpenBanking_CARD_V1.Data;
using OpenBanking_CARD_V1.Models;
using OpenBanking_CARD_V1.Repository;
using System.Threading.Tasks;

namespace OpenBanking_CARD_V1.Repository
{
    public class CardRepository : ICardRepository
    {
        private readonly AppDbContext _context;

        public CardRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Card>> GetAllCardsAsync()
        {
            return await _context.Cards
                .ToListAsync();
        }
        //
    }
}
