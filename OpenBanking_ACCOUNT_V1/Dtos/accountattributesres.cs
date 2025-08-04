namespace OpenBanking_ACCOUNT_V1.Dtos
{
    public class accountattributesres
    {
         public string product_code { get; set; }
     
        public string account_attribute_id { get; set; }
        public string name { get; set; }
        public Type type { get; set; }
        public string value { get; set; }
        public string product_instance_code { get; set; }
    }
}
