using dtso.core.Services;
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


        /// <summary>
        /// Flattens the given invoice into a "Basic Invoice" Reposne using the ginven account number and expense for the account
        /// </summary>
        public static InvoiceBasic MapFromObject(Invoice invoice, string accountNumber, decimal expense)
        {
            var basicInvoice = new InvoiceBasic()
            {
                InvoiceId = invoice.InvoiceId,
                InvoiceNumber = invoice.InvoiceNumber,
                InvoiceDate = invoice.InvoiceDate,
                InvoiceType = invoice.InvoiceType.Name,
                AccountNumber = accountNumber,
                Expense = expense,
                Description = invoice.Description,
            };

            basicInvoice.Vendor = new VendorListing()
            {
                VendorId = invoice.Vendor.VendorId,
                Name = invoice.Vendor.Name
            };

            return basicInvoice;
        }
    }
}
