using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenBanking_ACCOUNT_V1.Models
{   [Table("Agent")] 
    public class Agent
    {   [Key]
        public string agent_id { get; set; }
        [Column("bank_id")] 
        [JsonProperty("bank_id")]
        public string Bank_id { get; set; }
        public string legal_name { get; set; }
        public string mobile_phone_number { get; set; }
        public string agent_number { get; set; }
        public Currency currency { get; set; }
        public bool is_confirmed_agent { get; set; }
        public bool is_pending_agent { get; set; }
    }
}
