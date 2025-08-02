using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace OpenBanking_CARD_V1.Models
{
    public class Card
    {
        [Key]
        public string CardId { get; set; }

        [JsonProperty("bank_id")]
        public string BankId { get; set; }

        public string AccountId { get; set; }
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
        public DateTime Collected { get; set; }
        public DateTime Posted { get; set; }
        public string CustomerId { get; set; }

        // Relationships
        public Replacement Replacement { get; set; }
        public Card_attributes CardAttributes { get; set; }

        public List<Pin_reset> PinResets { get; set; } = new List<Pin_reset>();

        // Not mapped collections
        [NotMapped]
        public List<string> Networks { get; set; } = new();
        
        [NotMapped]
        public List<string> Allows { get; set; } = new();
    }
}
