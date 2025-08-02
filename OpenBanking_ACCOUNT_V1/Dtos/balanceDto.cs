using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using OpenBanking_ACCOUNT_V1.Models;
namespace OpenBanking_ACCOUNT_V1.Dtos

{
    public class balanceDto
    {
        public Currency currency { get; set; }
        public float amount { get; set; }
    
    }
}
