using System.Collections.Generic;
using Newtonsoft.Json;
using OpenBanking_ACCOUNT_V1.Models;
using System.ComponentModel.DataAnnotations;
using Type = OpenBanking_ACCOUNT_V1.Models.Type;
namespace OpenBanking_ACCOUNT_V1.Dtos
{
    public class account_attributesDto
    {
  
        public Type type { get; set; }
        [JsonProperty("code")]
        public string product_instance_code { get; set; }
        public string value { get; set; }
    }
}
