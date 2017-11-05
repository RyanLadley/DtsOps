using dtso.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dtso.api.Models.Forms
{
    public class InvoiceForm
    {
        public int? InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public string Description { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DatePaid { get; set; }
        public int InvoiceTypeId { get; set; }
        public int VendorId { get; set; }
        public List<InvoiceAccountForm> InvoiceAccounts { get; set; }

        //These Fields are only cent in the adjustments and are not mapped to a core object
        public List<int> CityExpensesToRemove { get; set; }
        public List<int> InvoiceAccountsToRemove { get; set; }
        
        public Invoice MapToCore()
        {
            var invoice = new Invoice()
            {
                InvoiceId = (InvoiceId.HasValue) ? InvoiceId.Value : 0,
                InvoiceNumber = this.InvoiceNumber,
                Description = this.Description,
                InvoiceDate = this.InvoiceDate,
                DatePaid = this.DatePaid,
                InvoiceType = new InvoiceType() { InvoiceTypeId = this.InvoiceTypeId },
                Vendor = new Vendor() { VendorId = VendorId }
            };

            invoice.AccountTotals = new List<InvoiceAccountTotal>();
            foreach(var account in this.InvoiceAccounts)
            {
                invoice.AccountTotals.Add(account.MapToCore());
            }

            return invoice;
        }
    }
}
