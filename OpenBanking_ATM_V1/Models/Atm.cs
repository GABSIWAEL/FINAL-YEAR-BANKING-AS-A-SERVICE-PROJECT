using System.Net;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace OpenBanking_ATM_V1.Models
{
    public class Atm
    {   
        [Key]
        public string Id { get; set; }
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
        public bool IsAccessible { get; set; }
        public string LocatedAt { get; set; }
        public string MoreInfo { get; set; }
        public string HasDepositCapability { get; set; }
        public List<Supported_languages> SupportedLanguages { get; set; }
        public List<Services> Services { get; set; } // Renamed to resolve ambiguity  
        public List<Accessibility_features> AccessibilityFeatures { get; set; }
        public List<Supported_currencies> SupportedCurrencies { get; set; }
        public List<Notes> Notes { get; set; }
        public List<Location_categories> LocationCategories { get; set; }
        public int MinimumWithdrawal { get; set; }
        public string BranchIdentification { get; set; }
        public string SiteIdentification { get; set; }
        public string SiteName { get; set; }
        public float CashWithdrawalNationalFee { get; set; }
        public float CashWithdrawalInternationalFee { get; set; }
        public float BalanceInquiryFee { get; set; }
        public string atm_type { get; set; }
        public string phone { get; set; }
        public  List<AtmAttributes> Attributes { get; set; }
    }
}
