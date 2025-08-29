using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
namespace OpenBanking_ACCOUNT_V1.Models
{
    public class Views_available
    {   [Key]
        public  string id { get; set; }
        public string short_name { get; set; }
        public string description { get; set; }
        public bool  is_public { get; set; } 
        public Alias alias { get; set; }
        public string Accountid { get; set; }
        public Account Account { get; set; }

        [JsonProperty("hide_metadata_if_alias_used")]
        public bool HideMetadataIfAliasUsed { get; set; }

        [JsonProperty("can_add_comment")]
        public bool CanAddComment { get; set; }

        [JsonProperty("can_add_corporate_location")]
        public bool CanAddCorporateLocation { get; set; }

        [JsonProperty("can_add_image")]
        public bool CanAddImage { get; set; }

        [JsonProperty("can_add_image_url")]
        public bool CanAddImageUrl { get; set; }

        [JsonProperty("can_add_more_info")]
        public bool CanAddMoreInfo { get; set; }

        [JsonProperty("can_add_open_corporates_url")]
        public bool CanAddOpenCorporatesUrl { get; set; }

        [JsonProperty("can_add_physical_location")]
        public bool CanAddPhysicalLocation { get; set; }

        [JsonProperty("can_add_private_alias")]
        public bool CanAddPrivateAlias { get; set; }

        [JsonProperty("can_add_public_alias")]
        public bool CanAddPublicAlias { get; set; }

        [JsonProperty("can_add_tag")]
        public bool CanAddTag { get; set; }

        [JsonProperty("can_add_url")]
        public bool CanAddUrl { get; set; }

        [JsonProperty("can_add_where_tag")]
        public bool CanAddWhereTag { get; set; }

        [JsonProperty("can_delete_comment")]
        public bool CanDeleteComment { get; set; }

        [JsonProperty("can_delete_corporate_location")]
        public bool CanDeleteCorporateLocation { get; set; }

        [JsonProperty("can_delete_image")]
        public bool CanDeleteImage { get; set; }

        [JsonProperty("can_delete_physical_location")]
        public bool CanDeletePhysicalLocation { get; set; }

        [JsonProperty("can_delete_tag")]
        public bool CanDeleteTag { get; set; }

        [JsonProperty("can_delete_where_tag")]
        public bool CanDeleteWhereTag { get; set; }

        [JsonProperty("can_edit_owner_comment")]
        public bool CanEditOwnerComment { get; set; }

        [JsonProperty("can_see_bank_account_balance")]
        public bool CanSeeBankAccountBalance { get; set; }

        [JsonProperty("can_see_bank_account_bank_name")]
        public bool CanSeeBankAccountBankName { get; set; }

        [JsonProperty("can_see_bank_account_currency")]
        public bool CanSeeBankAccountCurrency { get; set; }

        [JsonProperty("can_see_bank_account_iban")]
        public bool CanSeeBankAccountIban { get; set; }

        [JsonProperty("can_see_bank_account_label")]
        public bool CanSeeBankAccountLabel { get; set; }

        [JsonProperty("can_see_bank_account_national_identifier")]
        public bool CanSeeBankAccountNationalIdentifier { get; set; }

        [JsonProperty("can_see_bank_account_number")]
        public bool CanSeeBankAccountNumber { get; set; }

        [JsonProperty("can_see_bank_account_owners")]
        public bool CanSeeBankAccountOwners { get; set; }

        [JsonProperty("can_see_bank_account_swift_bic")]
        public bool CanSeeBankAccountSwiftBic { get; set; }

        [JsonProperty("can_see_bank_account_type")]
        public bool CanSeeBankAccountType { get; set; }

        [JsonProperty("can_see_comments")]
        public bool CanSeeComments { get; set; }

        [JsonProperty("can_see_corporate_location")]
        public bool CanSeeCorporateLocation { get; set; }

        [JsonProperty("can_see_image_url")]
        public bool CanSeeImageUrl { get; set; }

        [JsonProperty("can_see_images")]
        public bool CanSeeImages { get; set; }

        [JsonProperty("can_see_more_info")]
        public bool CanSeeMoreInfo { get; set; }

        [JsonProperty("can_see_open_corporates_url")]
        public bool CanSeeOpenCorporatesUrl { get; set; }

        [JsonProperty("can_see_other_account_bank_name")]
        public bool CanSeeOtherAccountBankName { get; set; }
    }
}
