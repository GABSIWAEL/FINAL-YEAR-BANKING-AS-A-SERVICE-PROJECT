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
    public class CreateAccountAttributeBodyDto
    {
        // YOU MUST RESPECT THE STATUS AND THE ALIGN OF THE ATTRIBUTES 
        // THIS IS THE DTO FOR THE BODY REQUEST 
        public string name { get; set; }
        public AttributeType type { get; set; }
        public string value { get; set; }
        public string product_instance_code { get; set; }
    }
}
