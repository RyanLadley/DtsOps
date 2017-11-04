using dtso.data.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace dtso.data.Entities
{
    public class Ticket
    {
        public int TicketId { get; set; }

        public int VendorId { get; set; }
        public Vendor Vendor { get; set; }

        public int AccountId { get; set; }
        public Account Account { get; set; }
        public vAccount vAccount { get; set; }

        public int MaterialVendorId { get; set; }
        public MaterialVendor MaterialVendor { get; set; }

        public string TicketNumber { get; set; }
        public DateTime Date { get; set; }
        public double Quantity { get; set; }
        public decimal Cost { get; set; }

        public int? InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
    }
}
