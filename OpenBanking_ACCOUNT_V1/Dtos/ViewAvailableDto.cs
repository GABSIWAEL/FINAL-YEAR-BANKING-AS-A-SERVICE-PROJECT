namespace OpenBanking_ACCOUNT_V1.Dtos
{
    public class ViewAvailableDto
    {
        public required string id { get; set; }
        public string short_name { get; set; }
        public bool  is_public { get; set; } 
    }
}
