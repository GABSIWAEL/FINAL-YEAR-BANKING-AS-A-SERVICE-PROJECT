using Grpc.Core;
using OpenBanking_ACCOUNT_V1.Data;
using OpenBanking_ACCOUNT_V1.Models;
using OpenBanking_ACCOUNT_V1.Repository;
using AutoMapper;
using AccountService;
using System.Threading.Tasks;

namespace OpenBanking_ACCOUNT_V1.SyncDataService.Grpc
{
    public class GrpcAccountService : GrpcAccount.GrpcAccountBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GrpcAccountService(IAccountRepository accountRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public override async Task<GetAccountsByIdsResponse> GetAccountsByIds(GetAccountsByIdsRequest request, ServerCallContext context)
        {
            var accounts = await _accountRepository.GetAccountsByIdsAsync(request.AccountIds);

            var response = new GetAccountsByIdsResponse();
            foreach (var account in accounts)
            {
                response.Accounts.Add(_mapper.Map<GrpcAccountModelForCardOfCurrentUser>(account));
            }
            return response;
        }
    }
}
 