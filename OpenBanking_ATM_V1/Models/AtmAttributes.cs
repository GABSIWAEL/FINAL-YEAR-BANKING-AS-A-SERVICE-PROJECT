using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace OpenBanking_ATM_V1.Models
{
    public class AtmAttributes
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonProperty("atm_attribute_id")]
        public string Id { get; set; }  // Unique ID for the attribute

        [JsonProperty("bank_id")]
        public string BankId { get; set; }

        [JsonProperty("atm_id")]
        public string AtmId { get; set; }  // Use PascalCase here

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public TypeAtm Type { get; set; }

        [JsonProperty("value")]
        public int Value { get; set; }

        [JsonProperty("is_active")]
        public bool IsActive { get; set; }
    }
}
