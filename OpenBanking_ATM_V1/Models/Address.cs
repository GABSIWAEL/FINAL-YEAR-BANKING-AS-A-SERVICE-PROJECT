using Newtonsoft.Json;

namespace OpenBanking_ATM_V1.Models
{
    public class Address
    {
        [JsonProperty("line_1")]
        public string Line1 { get; set; }
        [JsonProperty("line_2")]
        public string Line2 { get; set; }
        [JsonProperty("line_3")]
        public string Line3 { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string State { get; set; }
        public string Postcode { get; set; }
        [JsonProperty("country_code")]
        public string CountryCode { get; set; }
    }
}