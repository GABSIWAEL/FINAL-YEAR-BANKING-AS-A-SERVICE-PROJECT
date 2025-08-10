/*
THIS PROJECT IS CREATED BY WAEL GABSI 
WHATSAPP / +216 22152879 
GMAIL / waelwaelgabsi@gmail.com 
TELEGRAM / @GBWAEL 
*/
using System.Collections.Generic;
using Newtonsoft.Json;
using OpenBanking_ACCOUNT_V1.Models;
using System.ComponentModel.DataAnnotations;
using AttributeType = OpenBanking_ACCOUNT_V1.Models.AttributeType;
namespace OpenBanking_ACCOUNT_V1.Dtos
{
    public class accountattributesres
    {
         public string product_code { get; set; }
     
        public string account_attribute_id { get; set; }
        public string name { get; set; }
        public AttributeType type { get; set; }
        public string value { get; set; }
        public string product_instance_code { get; set; }
    }
}
