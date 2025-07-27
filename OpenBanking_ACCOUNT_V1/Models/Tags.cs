using System.ComponentModel.DataAnnotations;
namespace OpenBanking_ACCOUNT_V1.Models
{
    public class Tags
    {   [Key]
        public string id { get; set; }
        public Value Value { get; set; }
        public DateTime date { get; set; }
        public Owners user { get; set; }

    }
}
