using System.Collections.Generic;
using Newtonsoft.Json;
using OpenBanking_ACCOUNT_V1.Models;
namespace OpenBanking_ACCOUNT_V1.Dtos
{
    public class CreateAccountDto
    {
         public string user_id { get; set; }
        public string label { get; set; }
         public string product_code { get; set; }
         public List<Balance> balances { get; set; } = new List<Balance>();
         public string branch_id { get; set; }
         public List<Account_routings> account_routings { get; set; } = new List<Account_routings>();
    }
}
