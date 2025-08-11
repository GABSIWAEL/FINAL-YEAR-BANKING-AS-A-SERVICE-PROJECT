namespace OpenBanking_AUTHENTICATOR_V1.Models
{
    public enum BuisnessPlan
    {
        BASIC,     // Fintech companies or services using the system
        DENIED,       // Administrators with full control
        PRO,    // End users/customers
        SUPER,    // Providers of goods or services
        ELITE
    }
}
