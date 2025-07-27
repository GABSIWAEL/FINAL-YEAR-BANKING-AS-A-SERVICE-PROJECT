namespace OpenBanking_CARD_V1.Models
{
    public class Card_attributes
    {
        public String name { get; set; }
        public String card_id { get; set; }
        public String attribute_type { get; set; }
         [JsonProperty("bank_id")]
        public string BankId { get; set; }
     

        public String value { get; set; }

        public String card_attribute_id { get; set; }

    }

}


         