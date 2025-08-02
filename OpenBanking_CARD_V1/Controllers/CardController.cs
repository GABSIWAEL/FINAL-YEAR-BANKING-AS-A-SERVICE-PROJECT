using Microsoft.AspNetCore.Mvc;
using OpenBanking_CARD_V1.Repository;
using OpenBanking_CARD_V1.SyncDataService.Grpc;
using OpenBanking_CARD_V1.Data;
using OpenBanking_CARD_V1.Dtos;
using Grpc.Core;


namespace OpenBanking_CARD_V1.Controllers
{

    [ApiController]
    [Route("obp/v5.1.0/cards")]
    public class CardController : ControllerBase
    {
        private readonly ICardRepository _cardRepo;
        private readonly AccountGrpcService _accountService;

        public CardController(ICardRepository cardRepo, AccountGrpcService accountService)
        {
            _cardRepo = cardRepo;
            _accountService = accountService;
        }

        [HttpGet]
public async Task<ActionResult<IEnumerable<CardsForCurrentUser>>> GetCards()
{
    try
    {
        var cards = await _cardRepo.GetAllCardsAsync();
        var accountIds = cards.Select(c => c.AccountId).Distinct();

        var accounts = await _accountService.GetAccountsForCardsAsync(accountIds);

        var results = cards.Select(card =>
        {
            var account = accounts.FirstOrDefault(a => a.Id == card.AccountId);

            return new CardsForCurrentUser
            {
                BankId = card.BankId,
                BankCardNumber = card.CardNumber,
                NameOnCard = card.NameOnCard,
                IssueNumber = card.IssueNumber,
                SerialNumber = card.SerialNumber,
                ValidFromDate = card.ValidFromDate,
                ExpiresDate = card.ExpiresDate,
                Enabled = card.Enabled,
                Cancelled = card.Cancelled,
                OnHotList = card.OnHotList,
                Technology = card.Technology,
                Networks = card.Networks,
                Allows = card.Allows,
                Collected = card.Collected,
                Posted = card.Posted,
                Account = account
            };
        }).ToList();

        return Ok(new { cards = results });
    }
    catch (RpcException rpcEx)
    {
        Console.WriteLine($"❌ Erreur gRPC: {rpcEx.Status.Detail}");
        return StatusCode(500, $"Erreur gRPC: {rpcEx.Status.Detail}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Erreur inconnue: {ex.Message}");
        return StatusCode(500, "Erreur interne");
    }
}
    }}