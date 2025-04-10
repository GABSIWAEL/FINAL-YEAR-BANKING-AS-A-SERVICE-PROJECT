using Newtonsoft.Json;

namespace OpenBanking_BRANCH_V1.Models
{
    public class Branch
    {   public string Id { get; set; }
        [JsonProperty("bank_id")]
        public string BankId { get; set; }
        public string name { get; set; }
        // public Address address { get; set; }
        //public Location location { get; set; }
        //public Meta meta { get; set; }
        public  List<Lobby> lobby { get; set; }
        public List<Drive_up> drive_up { get; set; }
        public Branch_routing branch_routing { get; set; }
        public bool is_accessible { get; set; } 
        public string accessibleFeatures { get; set; }
        public string branch_type { get; set; }
        public string more_info { get; set; }
        public string phone_number { get; set; }


    }
}
