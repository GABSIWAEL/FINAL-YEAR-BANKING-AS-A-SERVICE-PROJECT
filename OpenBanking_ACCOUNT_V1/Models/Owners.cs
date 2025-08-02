using System.ComponentModel.DataAnnotations;
namespace OpenBanking_ACCOUNT_V1.Models
{
    public class Owners
    {
        [Key]
        public string id { get; set; }
        public string provider { get; set; }
        public string dispay_name { get; set; }
        public string Accountid { get; set; }
        public Account Account { get; set; }
    }
}
