using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace OpenBanking_ATM_V1.Models
{
    public class Location
    {   [Key]
        public string id  { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }
}

