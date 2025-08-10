using OpenBanking_ATM_V1.Models;
using System.Threading.Tasks;
using OpenBanking_ATM_V1.Dtos;

namespace OpenBanking_ATM_V1.Repository
{
    public interface IAtmRepository
    {
       
        Task<Atm> CreateAtm( string bankId, CreateAtmBody  createAtmBody);   // create atm 
      //  Task<AtmAttributes> CretaAtmAttributes (string  bankId , string atm_id , AtmAttributesBody AtmAttributesBody ); // create atm attributes 

    }
}