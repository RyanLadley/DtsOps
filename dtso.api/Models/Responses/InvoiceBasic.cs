using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dtso.api.Models.Responses
{
    public class InvoiceBasic
    {
        public int InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }

        public string AccountNumber { get; set; }
        public decimal Expense { get; set; }

        public VendorListing Vendor { get; set; }

        public string InvoiceType { get; set; }

        public DateTime InvoiceDate { get; set; }
        public string Description { get; set; }
    }
}
