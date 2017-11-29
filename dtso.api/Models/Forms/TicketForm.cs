using dtso.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dtso.api.Models.Forms
{
    public class TicketForm
    {
        public int TicketId { get; set; }
        public int VendorId { get; set; }
        public int AccountId { get; set; }
        public string TicketNumber { get; set; }
        public DateTime Date { get; set; }
        public MaterialForm Material { get; set; }
        public double Quantity { get; set; }
        public decimal Cost { get; set; }
        public int? InvoiceId { get; set; }
        
        public Ticket MapToCore()
        {
            return new Ticket()
            {
                TicketId = this.TicketId,
                VendorId = this.VendorId,
                AccountId = this.AccountId,
                MaterialVendorId = this.Material.MaterialVendorId,
                TicketNumber = this.TicketNumber,
                Date = this.Date,
                Quantity = this.Quantity,
                Cost = this.Cost,
                InvoiceId = this.InvoiceId
            };
        }

    }
}
