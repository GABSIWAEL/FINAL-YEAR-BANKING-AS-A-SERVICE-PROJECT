namespace OpenBanking_NOTIFICATION_V1.Shared.Events
{
    public class AccountAttributeCreatedEvent
    {
    public string account_attribute_id { get; set; }
    public string name { get; set; }
    public string value { get; set; }
    public string product_instance_code { get; set; }
    }
}
