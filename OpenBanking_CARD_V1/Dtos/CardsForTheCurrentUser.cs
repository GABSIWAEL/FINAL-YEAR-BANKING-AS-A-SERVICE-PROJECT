using AccountService;
using OpenBanking_CARD_V1.Models; // Assuming you add project reference to Account service DTOs

namespace OpenBanking_CARD_V1.Dtos
{
    public class CardsForCurrentUser
    {
        public string BankId { get; set; }
        public string BankCardNumber { get; set; }
        public string NameOnCard { get; set; }
        public string IssueNumber { get; set; }
        public string SerialNumber { get; set; }
        public DateTime ValidFromDate { get; set; }
        public DateTime ExpiresDate { get; set; }
        public bool Enabled { get; set; }
        public bool Cancelled { get; set; }
        public bool OnHotList { get; set; }
        public string Technology { get; set; }
        public List<string> Networks { get; set; }
        public List<string> Allows { get; set; }
        public GrpcAccountModelForCardOfCurrentUser Account { get; set; }
        public Replacement Replacement { get; set; }
        public List<Pin_reset> PinReset { get; set; }
        public DateTime Collected { get; set; }
        public DateTime Posted { get; set; }
    }
}
