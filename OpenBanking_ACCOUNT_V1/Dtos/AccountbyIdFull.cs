using System.Collections.Generic;
using Newtonsoft.Json;
using OpenBanking_ACCOUNT_V1.Models;
namespace OpenBanking_ACCOUNT_V1.Dtos

{
    public class AccountbyIdFull
    {
        public string id { get; set; } 
        public string label { get; set; }
        public int number { get; set; }
        public List<Owners> owners { get; set; }
        public string product_code { get; set; }
         public List<Balance> balances { get; set; }
        public List<Views_available> views_available { get; set; }
        [JsonProperty("bank_id")]
        public string Bank_id { get; set; }
        public List<Account_routings> account_routings { get; set; }
        
        public List<Tags> tags { get; set; }


    }
}
