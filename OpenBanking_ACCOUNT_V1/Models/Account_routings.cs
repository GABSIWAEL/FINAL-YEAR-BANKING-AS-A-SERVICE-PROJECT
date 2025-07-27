using System.ComponentModel.DataAnnotations;
namespace OpenBanking_ACCOUNT_V1.Models
{
    public class Account_routings
    {
        [Key]
        public int RoutingId { get; set; }
        public Scheme Scheme { get; set; }
        public string address { get; set; }

          public string Accountid { get; set; }
    public Account Account { get; set; }

    }
}
