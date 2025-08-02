using Newtonsoft.Json;
using OpenBanking_ACCOUNT_V1.Models;

namespace OpenBanking_ACCOUNT_V1.Dtos
{
    public class GrpcAccountModelForCardOfCurrentUser
    {
        public string id { get; set; }
        public string label { get; set; }
        public List<Views_available> views_available { get; set; } = new List<Views_available>();

        [JsonProperty("bank_id")]
        public string Bank_id { get; set; }
    }
}
