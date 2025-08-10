
/*
THIS PROJECT IS CREATED BY WAEL GABSI 
WHATSAPP / +216 22152879 
GMAIL / waelwaelgabsi@gmail.com 
TELEGRAM / @GBWAEL 
*/
using AutoMapper;
using OpenBanking_ATM_V1.Models;
using OpenBanking_ATM_V1.Dtos;

namespace OpenBanking_ATM_V1.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //-------- AUTO MAPPER ATM -------------------------------
            //******** Create Atm *********
            CreateMap<Atm , CreateAtmBody>();
            CreateMap<Atm , CreateAtmResponse>(); 
            
            //-------- AUTO MAPPER ATM ATTRIBUTES ---------------------
            //******** Create Atm Attributes *********
            CreateMap<AtmAttributes, AtmAttributesBody>();
            CreateMap<AtmAttributes, AtmAttributesResponse>();
        }
    }
}
