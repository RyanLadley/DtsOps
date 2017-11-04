using dtso.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dtso.api.Models.Responses
{
    public class TicketBasic
    {
        public int TicketId { get; set; }
        
        public VendorListing Vendor { get; set; }
        
        public AccountListing Account { get; set; }
        
        public MaterialVendorListing Material { get; set; }

        public string TicketNumber { get; set; }
        public DateTime Date { get; set; }
        public double Quantity { get; set; }
        public decimal Cost { get; set; }
        
        public InvoiceListing Invoice { get; set; }


        public static TicketBasic MapFromObject(Ticket obj)
        {
            return new TicketBasic()
            {
                TicketId = obj.TicketId,
                Vendor = VendorListing.MapFromObject(obj.Vendor),
                Account = AccountListing.MapFromObject(obj.Account),
                Material = MaterialVendorListing.MapFromObject(obj.MaterialVendor),
                TicketNumber = obj.TicketNumber,
                Quantity = obj.Quantity,
                Date = obj.Date,
                Cost = obj.Cost,
                Invoice = InvoiceListing.MapFromObject(obj.Invoice)
            };
        }
    }

}
