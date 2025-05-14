using Newtonsoft.Json;
using OpenBanking_ATM_V1.Models;

namespace OpenBanking_ATM_V1.Dtos
{
    public class BankATM
    {
        public string Id { get; set; }
        [JsonProperty("bank_id")]
        public string BankId { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }

        public Location Location { get; set; }
        public Meta Meta { get; set; }
        public OpeningHours Monday { get; set; }
        public OpeningHours Tuesday { get; set; }
        public OpeningHours Wednesday { get; set; }
        public OpeningHours Thursday { get; set; }
        public OpeningHours Friday { get; set; }
        public OpeningHours Saturday { get; set; }
        public OpeningHours Sunday { get; set; }
        [JsonProperty("is_accessible")]
        public string IsAccessible { get; set; }
        [JsonProperty("located_at")]
        public string LocatedAt { get; set; }
        [JsonProperty("more_info")]
        public string MoreInfo { get; set; }
        [JsonProperty("has_deposit_capability")]
        public string HasDepositCapability { get; set; }
        public List<Supported_languages> SupportedLanguages { get; set; }
        public List<Services> services { get; set; }
        public List<Accessibility_features> accessibility_Features { get; set; }
        public List<Supported_currencies> supported_Currencies { get; set; }
        public List<Notes> notes { get; set; }
        public List<Location_categories> location_Categories { get; set; }
        public int minimum_withdrawal { get; set; }
        public string branch_identification { get; set; }
        public string site_identification { get; set; }

        public string site_name { get; set; }
        public float cash_withdrawal_national_fee { get; set; }
        public float cash_withdrawal_international_fee { get; set; }
        public float balance_inquiry_fee { get; set; }
        public Type atm_type { get; set; }
        public int phone { get; set; }
        public List<ATMAttributes> attributes { get; set; }


    }
}
