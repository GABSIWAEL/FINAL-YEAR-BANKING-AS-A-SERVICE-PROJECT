namespace OpenBanking_NOTIFICATION_V1.Shared.Events
{
    public class AtmAttributeCreatedEvent
    {
     public string BankId { get; set; }
        public string AtmId { get; set; }
        public string AttributeId { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public bool IsActive { get; set; }
    }
    
}
