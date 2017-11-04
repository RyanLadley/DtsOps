using System;
using System.Collections.Generic;
using System.Text;

namespace dtso.core.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }

        public List<InvoiceAccountTotal> AccountTotals { get; set; }
        public Vendor Vendor { get; set; }
        public InvoiceType InvoiceType { get; set; }

        public DateTime InvoiceDate { get; set; }
        public DateTime DatePaid { get; set; }
        public string Description { get; set; }

        public static Invoice MapFromEntity(data.Entities.Invoice entity)
        {
            if (entity == null)
                return null;

            var invoice = new Invoice()
            {
                InvoiceId = entity.InvoiceId,
                InvoiceNumber = entity.InvoiceNumber,
                Vendor = Vendor.MapFromEntity(entity.Vendor),
                InvoiceType = InvoiceType.MapFromEntity(entity.InvoiceType),
                InvoiceDate = entity.InvoiceDate,
                DatePaid = entity.DatePaid,
                Description = entity.Description

            };

            invoice.AccountTotals = new List<InvoiceAccountTotal>();
            foreach(var invoiceAccount in entity.InvoiceAccounts)
            {
                invoice.AccountTotals.Add(InvoiceAccountTotal.MapFromEntity(invoiceAccount));
            }

            return invoice;
        }

        public data.Entities.Invoice MapToEntity()
        {
            var entity = new data.Entities.Invoice()
            {
                InvoiceId = this.InvoiceId,
                InvoiceNumber = this.InvoiceNumber,
                VendorId = this.Vendor.VendorId,
                InvoiceTypeId = this.InvoiceType.InvoiceTypeId,
                InvoiceDate = this.InvoiceDate,
                DatePaid = this.DatePaid,
                Description = this.Description,
            };

            entity.InvoiceAccounts = new List<data.Entities.InvoiceAccount>();
            foreach(var account in AccountTotals)
            {
                entity.InvoiceAccounts.Add(account.MapToEntity(this.InvoiceId));
            }

            return entity;
        }
    }
}
