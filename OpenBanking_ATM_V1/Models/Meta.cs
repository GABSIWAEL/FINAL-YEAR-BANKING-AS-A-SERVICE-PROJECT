using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace OpenBanking_ATM_V1.Models
{
    public class Meta
    {   [Key]
        public string id  { get; set; }
        public Licence licence { get; set; }
    }
}