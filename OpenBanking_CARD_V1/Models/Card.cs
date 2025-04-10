using Newtonsoft.Json;
namespace OpenBanking_CARD_V1.Models
{
    public class Card
    {
        public string CardId { get; set; }

        [JsonProperty("bank_id")]
        public string BankId { get; set; }

        public string CardNumber { get; set; }
        public string CardType { get; set; }
        public string NameOnCard { get; set; }
        public string IssueNumber { get; set; }
        public string SerialNumber { get; set; }
        public DateTime ValidFromDate { get; set; }
        public DateTime ExpiresDate { get; set; }
        public bool Enabled { get; set; }
        public bool Cancelled { get; set; }
        public bool OnHotList { get; set; }
        public string Technology { get; set; }

        public List<Networks> Networks { get; set; }
        public List<Allows> Allows { get; set; }

        // public Account Account { get; set; } // from another microservice
        public Replacement Replacement { get; set; }
        public Pin_reset PinReset { get; set; }

        public DateTime Collected { get; set; }
        public DateTime Posted { get; set; }

        public string CustomerId { get; set; }

        public Card_attributes CardAttributes { get; set; }

        // Finalized Card model based on Open Banking standard
    }
}
