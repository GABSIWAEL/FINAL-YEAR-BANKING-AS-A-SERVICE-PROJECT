using Newtonsoft.Json;
using OpenBanking_ATM_V1.Models;

namespace OpenBanking_ATM_V1.Dtos
{
    public class AtmAttributesBody
    {
        public string name { get; set; }
        public TypeAtm type { get; set; }
        public int value { get; set; }
        public bool is_active { get; set; }
     

    }
}
