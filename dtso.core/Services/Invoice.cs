using System;
using System.Collections.Generic;
using System.Text;

namespace dtso.core.Services
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }

        public List<InvoiceAccountTotal> AccountTotals { get; set; }
        public Vendor Vendor { get; set; }
        public InvoiceType InvoiceType { get; set; }

        public DateTime InvoiceDate { get; set; }
        public string Description { get; set; }

        public static Invoice MapFromEntity(data.Entities.Invoice entity)
        {
            var invoice = new Invoice()
            {
                InvoiceId = entity.InvoiceId,
                InvoiceNumber = entity.InvoiceNumber,
                Vendor = Vendor.MapFromEntity(entity.Vendor),
                InvoiceType = InvoiceType.MapFromEntity(entity.InvoiceType),
                InvoiceDate = entity.InvoiceDate,
                Description = entity.Description

            };

            invoice.AccountTotals = new List<InvoiceAccountTotal>();
            foreach(var invoiceAccount in entity.InvoiceAccounts)
            {
                invoice.AccountTotals.Add(InvoiceAccountTotal.MapFromEntity(invoiceAccount));
            }

            return invoice;
        }
    }
}
