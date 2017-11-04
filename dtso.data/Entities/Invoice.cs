using System;
using System.Collections.Generic;
using System.Text;

namespace dtso.data.Entities
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }

        public List<InvoiceAccount> InvoiceAccounts { get; set; }

        public int VendorId { get; set; }
        public Vendor Vendor { get; set; }

        public int InvoiceTypeId { get; set; }
        public InvoiceType InvoiceType { get; set; }

        public DateTime InvoiceDate { get; set; }
        public DateTime DatePaid { get; set; }
        public string Description { get; set; }

        public List<Ticket> Tickets { get; set; }

    }
}
