using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OpenBanking_ACCOUNT_V1.Models
{
    public class Account
    {
        [Key]
        public string id { get; set; }
        public string? user_id { get; set; }
        public string? label { get; set; }
        public int? number { get; set; }
        public string? product_code { get; set; }
        public string? account_type { get; set; }
        public string? branch_id { get; set; }

        [JsonProperty("bank_id")]
        public string Bank_id { get; set; }

        public List<Owners> owners { get; set; } = new List<Owners>();
        public List<Balance> balances { get; set; } = new List<Balance>();
        public List<Account_routings> account_routings { get; set; } = new List<Account_routings>();
        public List<Account_attributes> account_Attributes { get; set; } = new List<Account_attributes>();
        public List<Views_available> views_available { get; set; } = new List<Views_available>();
        public List<Tags> tags { get; set; } = new List<Tags>();
        
    }
}
