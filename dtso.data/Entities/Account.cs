using System;
using System.Collections.Generic;
using System.Text;

namespace dtso.data.Entities
{
    //This Class is the see for the "Accounts" view. It should rarely get called  
    public class Account
    {
        public int AccountId { get; set; }
        public int? SubNo { get; set; }
        public int? ShredNo { get; set; }
        public string Description { get; set; }
        public decimal AnnualBudget { get; set; }
        
        public int RegionalAccountCodeId { get; set; }
        public RegionalAccountCode RegionalAccountCode { get; set; }

        public List<InvoiceAccount> InvoiceAccounts { get; set; }
        public List<Ticket> Tickets { get; set; }

        public List<Transfer> TransfersFrom { get; set; }
        public List<Transfer> TransfersTo { get; set; }
    }
}
