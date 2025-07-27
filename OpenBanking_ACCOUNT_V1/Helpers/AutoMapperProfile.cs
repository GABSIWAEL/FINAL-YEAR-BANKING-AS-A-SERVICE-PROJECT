using AutoMapper;
using OpenBanking_ACCOUNT_V1.Models;
using OpenBanking_ACCOUNT_V1.Dtos;

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
            CreateMap<Balance, Balance>();
            CreateMap<Views_available, Views_available>();
            CreateMap<Account_routings, Account_routings>();
            CreateMap<Account_attributes, Account_attributes>();
            CreateMap<Tags, Tags>();
            CreateMap<Views_available, ViewAvailableDto>();
            CreateMap<Agent, Agent>();
            CreateMap<Account , Accounts_at_all_BanksPrivate>();
            CreateMap<Account , AccountsHeld>();
            CreateMap<Agent, Agents_at_Bank>();
            CreateMap<Account , FastFirehoseAccountsAtBank>();
            CreateMap<Agent , Agent>();

              

        }
    }
}
