using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using OpenBanking_ACCOUNT_V1.Models;
namespace OpenBanking_ACCOUNT_V1.Dtos

{
    public class AccountsHeld
    {
        public string id { get; set; } 
        public string label { get; set; }
        [JsonProperty("bank_id")]
        public string Bank_id { get; set; }
        public int number { get; set; }
        public List<Account_routings> account_routings { get; set; }

    }
}
