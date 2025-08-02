using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace OpenBanking_CARD_V1.Models
{
    public class Card_attributes
    {
        [Key]
        public string Id { get; set; }

        public string Name { get; set; }

        [Required]
        public string CardId { get; set; }

        [ForeignKey("CardId")]
        public Card Card { get; set; }

        public string AttributeType { get; set; }

        [JsonProperty("bank_id")]
        public string BankId { get; set; }

        public string Value { get; set; }

        public string CardAttributeId { get; set; }
    }
}
