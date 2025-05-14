using Newtonsoft.Json;

namespace OpenBanking_ATM_V1.Models
{
    public class AtmAttributes
    {
        
        [JsonProperty("bank_id")]
        public string BankId { get; set; }
        public string atm_attribute_id { get; set; }
        public string name { get; set; }
        public Type type { get; set; }
        public int value { get; set; }
        public bool is_active { get; set; }

    }
}
