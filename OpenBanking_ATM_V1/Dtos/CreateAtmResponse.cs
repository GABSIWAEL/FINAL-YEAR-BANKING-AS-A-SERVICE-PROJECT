using OpenBanking_ATM_V1.Models;
using System.Collections.Generic;

namespace OpenBanking_ATM_V1.Dtos
{
    public class CreateAtmResponse
    {
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
        public List<Services> Services { get; set; }
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
        public string AtmType { get; set; }
        public string Phone { get; set; }
        public List<AtmAttributes> Attributes { get; set; }
    }
}
