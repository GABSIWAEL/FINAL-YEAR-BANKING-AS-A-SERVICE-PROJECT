using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenBanking_CARD_V1.Models
{
    public class Pin_reset
    {
        [Key]
        public string Id { get; set; }

        public DateTime RequestedDate { get; set; }

        public string Reason_requested { get; set; }

        [Required]
        public string CardId { get; set; }

        [ForeignKey("CardId")]
        public Card Card { get; set; }
    }
}
