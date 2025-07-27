using System.Collections.Generic;
using Newtonsoft.Json;
using OpenBanking_ACCOUNT_V1.Models;
namespace OpenBanking_ACCOUNT_V1.Dtos
{
    public class AccountsAtBank
    {
        public string id { get; set; } 
        public string label { get; set; }
        [JsonProperty("bank_id")]
        public string Bank_id { get; set; }
        public List<ViewAvailableDto> views_available { get; set; }
    }
}
