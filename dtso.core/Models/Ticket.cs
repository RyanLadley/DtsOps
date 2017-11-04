using System;
using System.Collections.Generic;
using System.Text;

namespace dtso.core.Models
{
    public class Ticket
    {
        public int TicketId { get; set; }

        public int VendorId { get; set; }
        public Vendor Vendor { get; set; }

        public int AccountId { get; set; }
        public Account Account { get; set; }

        public int MaterialVendorId { get; set; }
        public MaterialVendor MaterialVendor { get; set; }

        public string TicketNumber { get; set; }
        public DateTime Date { get; set; }
        public double Quantity { get; set; }
        public decimal Cost { get; set; }

        public int? InvoiceId { get; set; }
        public Invoice Invoice { get; set; }

        public static Ticket MapFromEntity(data.Entities.Ticket entity)
        {
            return new Ticket()
            {
                TicketId = entity.TicketId,
                VendorId = entity.VendorId,
                Vendor = Vendor.MapFromEntity(entity.Vendor),
                AccountId = entity.AccountId,
                Account = Account.MapFromObject(entity.vAccount),
                MaterialVendorId = entity.MaterialVendorId,
                MaterialVendor = MaterialVendor.MapFromEntity(entity.MaterialVendor),
                TicketNumber = entity.TicketNumber,
                Date = entity.Date,
                Quantity = entity.Quantity,
                Cost = entity.Cost,
                InvoiceId = entity.InvoiceId,
                Invoice = Invoice.MapFromEntity(entity.Invoice)
            };
        } 

        public data.Entities.Ticket MapToEntity()
        {
            return new data.Entities.Ticket()
            {
                TicketId = this.TicketId,
                VendorId = this.VendorId,
                AccountId = this.AccountId,
                MaterialVendorId = this.MaterialVendorId,
                Date = this.Date,
                Quantity = this.Quantity,
                Cost = this.Cost,
                InvoiceId = this.InvoiceId,
                TicketNumber = this.TicketNumber
            };
        }
    }
}
