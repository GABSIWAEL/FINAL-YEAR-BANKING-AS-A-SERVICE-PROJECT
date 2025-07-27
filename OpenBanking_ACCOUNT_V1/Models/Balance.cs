using System.ComponentModel.DataAnnotations;
namespace OpenBanking_ACCOUNT_V1.Models
{
    public class Balance
    {   
         [Key]
        public int BalanceId { get; set; }
        public Currency currency { get; set; }
        public float amount { get; set; }

        public string Accountid { get; set; }
    public Account Account { get; set; }
    }
}
