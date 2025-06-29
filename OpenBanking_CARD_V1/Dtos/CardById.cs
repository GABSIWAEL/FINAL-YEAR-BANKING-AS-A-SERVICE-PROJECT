namespace OpenBanking_CARD_V1.Dtos
{
     public class CardById
    {
        public string CardId { get; set; }
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

        public List<string> Networks { get; set; }
        public List<string> Allows { get; set; }

        public AccountDto Account { get; set; }
        public ReplacementDto Replacement { get; set; }
        public List<PinResetDto> PinReset { get; set; }

        public DateTime Collected { get; set; }
        public DateTime Posted { get; set; }
        public string CustomerId { get; set; }

        public List<CardAttributesDto> CardAttributes { get; set; }
    }
}
