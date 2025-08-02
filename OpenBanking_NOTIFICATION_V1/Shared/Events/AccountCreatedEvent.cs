namespace OpenBanking_NOTIFICATION_V1.Shared.Events
{
    public class AccountCreatedEvent
    {
    public string AccountId { get; set; }
    public string UserId { get; set; }
    public string Label { get; set; }
    }
}
