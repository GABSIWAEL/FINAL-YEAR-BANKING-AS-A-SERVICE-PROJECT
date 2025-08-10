using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace OpenBanking_ATM_V1.Models
{
    public class OpeningHours
    {   [Key]
        public string id  { get; set; }
        [JsonProperty("opening_time")]
        public string OpeningTime { get; set; }
        [JsonProperty("closing_time")]
        public string ClosingTime { get; set; }
    }
}