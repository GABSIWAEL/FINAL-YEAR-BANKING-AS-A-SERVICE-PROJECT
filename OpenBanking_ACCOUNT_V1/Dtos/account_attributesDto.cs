using System.Collections.Generic;
using Newtonsoft.Json;
using OpenBanking_ACCOUNT_V1.Models;
using System.ComponentModel.DataAnnotations;
using AttributeType = OpenBanking_ACCOUNT_V1.Models.AttributeType;
namespace OpenBanking_ACCOUNT_V1.Dtos
{
    public class account_attributesDto
    {
  
        public AttributeType type { get; set; }
        [JsonProperty("code")]
        public string product_instance_code { get; set; }
        public string value { get; set; }
    }
}
