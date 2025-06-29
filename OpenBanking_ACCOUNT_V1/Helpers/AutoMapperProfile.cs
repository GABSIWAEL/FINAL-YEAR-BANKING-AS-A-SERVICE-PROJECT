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
            CreateMap<Owners, Owners>();
            CreateMap<Balance, Balance>();
            CreateMap<Views_available, Views_available>();
            CreateMap<Account_routings, Account_routings>();
            CreateMap<Account_attributes, Account_attributes>();
            CreateMap<Tags, Tags>();
        }
    }
}
