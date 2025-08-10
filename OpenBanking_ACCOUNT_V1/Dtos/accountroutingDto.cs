/*
THIS PROJECT IS CREATED BY WAEL GABSI 
WHATSAPP / +216 22152879 
GMAIL / waelwaelgabsi@gmail.com 
TELEGRAM / @GBWAEL 
*/
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using OpenBanking_ACCOUNT_V1.Models;
namespace OpenBanking_ACCOUNT_V1.Dtos

{
    public class accountroutingDto
    {
       public Scheme Scheme { get; set; }
        public string address { get; set; }
    
    }
}
