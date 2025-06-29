using System.ComponentModel.DataAnnotations;
namespace OpenBanking_ACCOUNT_V1.Models
{
    public class Balance
    {   
        [Key]
        public Currency currency { get; set; }
        public float amount { get; set; }
    }
}
