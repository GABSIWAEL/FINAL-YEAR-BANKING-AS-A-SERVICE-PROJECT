using AutoMapper;
using OpenBanking_CARD_V1.Dtos;
using OpenBanking_CARD_V1.Models;

namespace OpenBanking_CARD_V1.Helper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Card, CardsForCurrentUser>()
                .ForMember(dest => dest.BankCardNumber, opt => opt.MapFrom(src => src.CardNumber));
            // Account will be set manually after fetching from Account microservice
        }
    }
}
