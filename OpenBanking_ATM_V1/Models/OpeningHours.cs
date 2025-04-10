using Newtonsoft.Json;

namespace OpenBanking_ATM_V1.Models
{
    public class OpeningHours
    {
        [JsonProperty("opening_time")]
        public string OpeningTime { get; set; }
        [JsonProperty("closing_time")]
        public string ClosingTime { get; set; }
    }
}