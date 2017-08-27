
using dtso.data.Entities;
using System.Collections.Generic;

namespace dtso.data.Views
{
    public class vAccount
    {
        public int AccountId { get; set; }
        public int AccountNumber { get; set; }
        public int? SubNo { get; set; }
        public int? ShredNo { get; set; }
        public string Description { get; set; }
        public decimal AnnualBudget { get; set; }

        public int FundNumber { get; set; }
        public string ProjectNumber { get; set; }
        public string ProjectDescription { get; set; }
        public string AccountPrefix { get; set; }

        public int RegionalAccountCodeId { get; set; }

        public List<InvoiceAccount> InvoiceAccounts { get; set; }
    }
}
