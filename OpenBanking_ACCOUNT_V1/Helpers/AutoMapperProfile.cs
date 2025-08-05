
/*
THIS PROJECT IS CREATED BY WAEL GABSI 
WHATSAPP / +216 22152879 
GMAIL / waelwaelgabsi@gmail.com 
TELEGRAM / @GBWAEL 
*/
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
            //--------------------------------ACCOUNTS MAPPING DETAILS --------------------------------------
            CreateMap<Account, AccountbyIdFull>();
            CreateMap<Account, AccountBalancesByBANK_ID>();
            CreateMap<Account, AccountsAtBank>();
            CreateMap<Account, Accounts_at_all_BanksPrivate>();
            CreateMap<Account, AccountsHeld>();
            CreateMap<Account, CreateAccountDto>();
            CreateMap<Account, FastFirehoseAccountsAtBank>();
            CreateMap<Account, CreateAccountResponseDto>();

            //-----------------------------------ACCOUNT ATTRIBUTES  MAPPING DETAILS -----------------------------------
            CreateMap<Account_attributes , CreateAccountAttributeResponseDto>();
            CreateMap<Account_attributes , CreateAccountAttributeBodyDto>();
            CreateMap<Account_attributes, Account_attributes>();
            CreateMap<Account_attributes, accountattributesres>();

            //-----------------------------------ACCOUNT ROUTINGS   MAPPING DETAILS -----------------------------------
            CreateMap<Account_routings, Account_routings>();
            CreateMap<Account_routings, accountroutingDto>();
            CreateMap<Dtos.accountroutingDto, Models.Account_routings>();

      
            //-----------------------------------BALANCES MAPPING DETAILS -----------------------------------
            
            CreateMap<Models.Balance, Models.Balance>();
            CreateMap<Models.Balance, Dtos.balanceDto>();
            CreateMap<Dtos.balanceDto, Models.Balance>();

            //-----------------------------------VIEWS AVAILBALE  MAPPING DETAILS -----------------------------------

            CreateMap<Views_available, Views_available>();
            CreateMap<Views_available, ViewAvailableDto>();

            //-----------------------------------TAGS   MAPPING DETAILS -----------------------------------           
            CreateMap<Tags, Tags>();

            //-----------------------------------AGENTS  MAPPING DETAILS -----------------------------------
            CreateMap<Agent, Agent>();
            CreateMap<Agent, Agents_at_Bank>();
            CreateMap<Agent, Agent>();

            //-----------------------------------OWNERS   MAPPING DETAILS -----------------------------------
            CreateMap<Owners, Owners>();

            //-----------------------------------GRPC PROTOCOL BUFFER   MAPPING DETAILS -----------------------------------
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
