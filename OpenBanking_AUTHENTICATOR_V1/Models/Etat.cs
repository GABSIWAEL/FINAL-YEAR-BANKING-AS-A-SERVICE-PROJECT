namespace OpenBanking_AUTHENTICATOR_V1.Models
{
    public enum Etat
    {
        FINTECH,     // Fintech companies or services using the system
        ADMIN,       // Administrators with full control
        CUSTOMER,    // End users/customers
        SUPPLIER,    // Providers of goods or services
        PARTNER,     // Business partners with shared resources or data
        AGENT,       // Intermediaries or agents
        USER 
    }
}
