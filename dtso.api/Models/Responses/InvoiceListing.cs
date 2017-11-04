using dtso.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dtso.api.Models.Responses
{
    public class InvoiceListing
    {
        public int InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        
        public decimal Expense { get; set; }

        public VendorListing Vendor { get; set; }

        public InvoiceTypeListing InvoiceType { get; set; }

        public DateTime InvoiceDate { get; set; }
        public DateTime DatePaid { get; set; }

        public string Description { get; set; }


        /// <summary>
        /// Flattens the given invoice into a "Basic Invoice" Reposne using the ginven account number and expense for the account
        /// </summary>
        public static InvoiceListing MapFromObject(Invoice invoice)
        {
            if (invoice == null)
                return null;

            //Calculate Expense of invoice

            decimal expense = 0;
            foreach(var cost in invoice.AccountTotals)
            {
                expense += cost.Expense;
            }

            var listing = new InvoiceListing()
            {
                InvoiceId = invoice.InvoiceId,
                InvoiceNumber = invoice.InvoiceNumber,
                InvoiceDate = invoice.InvoiceDate,
                DatePaid = invoice.DatePaid,
                InvoiceType = InvoiceTypeListing.MapFromObject(invoice.InvoiceType),
                Expense = expense,
                Description = invoice.Description,
                Vendor = VendorListing.MapFromObject(invoice.Vendor)
            };
        
            return listing;
        }
    }
}
