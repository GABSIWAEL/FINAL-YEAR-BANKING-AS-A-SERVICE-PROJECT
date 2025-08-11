using System.ComponentModel.DataAnnotations;

using OpenBanking_AUTHENTICATOR_V1.Models;
namespace OpenBanking_AUTHENTICATOR_V1.Dtos
{
    public class PlanValidator
    {
        
       
public BuisnessPlan buisnessPlan { get; set; } = BuisnessPlan.DENIED;
        
    }
}