using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace OpenBanking_ACCOUNT_V1.Models
{
    public class Account

    {
        
        public string id { get; set; } 
        public string label { get; set; }
        public int number { get; set; }
        public List<Owners> owners { get; set; }
        public string product_code { get; set; }
        public Balance balance { get; set; }
        public Views_available views_available { get; set; }
        [JsonProperty("bank_id")]
        public string Bank_id { get; set; }
        public Account_routings account_routings { get; set; }
        public List<Account_attributes> account_Attributes { get; set; }
        public List<Tags> tags { get; set; }


            


    }
}

