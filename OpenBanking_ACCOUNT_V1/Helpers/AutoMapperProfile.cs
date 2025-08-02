using AutoMapper;
using OpenBanking_ACCOUNT_V1.Models;
using OpenBanking_ACCOUNT_V1.Dtos;
using AccountService;

namespace OpenBanking_ACCOUNT_V1.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Account, AccountbyIdFull>();
            CreateMap<Account, AccountBalancesByBANK_ID>();
            CreateMap<Account, AccountsAtBank>();
            CreateMap<Owners, Owners>();
            CreateMap<Models.Balance, Models.Balance>();
            CreateMap<Views_available, Views_available>();
            CreateMap<Account_routings, Account_routings>();
            CreateMap<Account_attributes, Account_attributes>();
            CreateMap<Tags, Tags>();
            CreateMap<Views_available, ViewAvailableDto>();
            CreateMap<Agent, Agent>();
            CreateMap<Account, Accounts_at_all_BanksPrivate>();
            CreateMap<Account, AccountsHeld>();
            CreateMap<Agent, Agents_at_Bank>();
            CreateMap<Account, FastFirehoseAccountsAtBank>();
            CreateMap<Agent, Agent>();
            CreateMap<Account, CreateAccountDto>();
            CreateMap<Account, CreateAccountResponseDto>();

            // ✅ gRPC mapping for Cards
            CreateMap<Account, AccountService.GrpcAccountModelForCardOfCurrentUser>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id))
                .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.label))
                .ForMember(dest => dest.BankId, opt => opt.MapFrom(src => src.Bank_id))
                .ForMember(dest => dest.ViewsAvailable, opt => opt.MapFrom(src => src.views_available));

            // ✅ Additional needed mappings
            CreateMap<Views_available, AccountService.ViewsAvailable>();
            CreateMap<OpenBanking_ACCOUNT_V1.Models.Alias, AccountService.Alias>();

            // ❗️THIS was missing and causing your crash:
            CreateMap<OpenBanking_ACCOUNT_V1.Dtos.GrpcAccountModelForCardOfCurrentUser, AccountService.GrpcAccountModelForCardOfCurrentUser>();
        }
    }
}
