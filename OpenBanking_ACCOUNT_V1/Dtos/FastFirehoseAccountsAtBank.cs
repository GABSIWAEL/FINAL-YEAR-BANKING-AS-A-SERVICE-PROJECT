using System.Collections.Generic;
using Newtonsoft.Json;
using OpenBanking_ACCOUNT_V1.Models;
namespace OpenBanking_ACCOUNT_V1.Dtos
{
    public class FastFirehoseAccountsAtBank
    {
         public string id { get; set; } 
        [JsonProperty("bank_id")]
        public string Bank_id { get; set; }
        
        public string label { get; set; }
        public int number { get; set; }
        public List<Owners> owners { get; set; }
        public string product_code { get; set; }
        public List<Balance> balances { get; set; }
        public List<Account_routings> account_routings { get; set; }
        public List<account_attributesDto> account_Attributes { get; set; }

    }
}
