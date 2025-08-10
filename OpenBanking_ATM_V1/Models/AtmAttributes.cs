using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace OpenBanking_ATM_V1.Models
{
    public class AtmAttributes
    {   [Key]
        string id  { get; set; }
        [JsonProperty("bank_id")]
        public string BankId { get; set; }
        public string atm_id { get; set; }
        public string atm_attribute_id { get; set; }
        public string name { get; set; }
        public TypeAtm type { get; set; }
        public int value { get; set; }
        public bool is_active { get; set; }

    }
}
