using AccountService;
using Grpc.Net.Client;

namespace OpenBanking_CARD_V1.SyncDataService.Grpc
{
    public class AccountGrpcService
    {
        private readonly IConfiguration _configuration;

        public AccountGrpcService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<GrpcAccountModelForCardOfCurrentUser>> GetAccountsForCardsAsync(IEnumerable<string> accountIds)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            var channel = GrpcChannel.ForAddress(_configuration["GrpcAccount"]);
            var client = new GrpcAccount.GrpcAccountClient(channel);

            var request = new GetAccountsByIdsRequest();
            request.AccountIds.AddRange(accountIds);

            var response = await client.GetAccountsByIdsAsync(request);

            return response.Accounts.ToList();
        }


    }
}