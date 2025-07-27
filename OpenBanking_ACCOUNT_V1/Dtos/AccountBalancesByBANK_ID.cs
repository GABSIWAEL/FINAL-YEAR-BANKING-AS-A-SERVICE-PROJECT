using Newtonsoft.Json;                  // For JsonProperty
using OpenBanking_ACCOUNT_V1.Models;
using System.Collections.Generic;
namespace OpenBanking_ACCOUNT_V1.Dtos

{
    public class AccountBalancesByBANK_ID
    {
        [JsonProperty("bank_id")]
        public string Bank_id { get; set; }
        [JsonProperty("account_id")]
        public string id { get; set; }
        public List<Account_routings> account_routings { get; set; }
        public string label { get; set; }
        public List<Balance> balances { get; set; }
    
    }
}
