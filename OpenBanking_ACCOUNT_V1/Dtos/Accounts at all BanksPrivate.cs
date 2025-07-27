using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using OpenBanking_ACCOUNT_V1.Models;
namespace OpenBanking_ACCOUNT_V1.Dtos
{
    public class Accounts_at_all_BanksPrivate
    {
         public string id { get; set; } 
        public string label { get; set; }
         [JsonProperty("bank_id")]
        public string Bank_id { get; set; }
        public string account_type { get; set; }
        public List<Account_routings> account_routings { get; set; }
         [JsonProperty("views")]
        public List<ViewAvailableDto> views_available { get; set; }
        
    }
}
