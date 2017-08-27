using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dtso.api.Models.Responses
{
    public class AccountOverview
    {
        public int AccountNumber { get; set; }
        public int? SubNo { get; set; }
        public int? ShredNo { get; set; }
        public string Description { get; set; }
        public decimal AnnualBudget { get; set; }
        
        public List<AccountOverview> Subaccounts { get; set; }
    }
}
