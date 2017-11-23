using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dtso.api.Models.Forms
{
    public class AccountForm
    {
        public int AccountId { get; set; }
        public string AccountNumber { get; set; }
        public string SubNo { get; set; }
        public string ShredNo { get; set; }
        public string Description { get; set; }
        public decimal AnnualBudget { get; set; }

        public List<AccountForm> ChildAccounts { get; set; }
        public List<AccountForm> NewChildAccounts { get; set; }

    }
}
