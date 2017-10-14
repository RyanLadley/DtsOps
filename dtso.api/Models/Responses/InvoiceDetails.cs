using dtso.core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dtso.api.Models.Responses
{
    public class InvoiceDetails
    {
        public int InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }

        public List<InvoiceExpense> Expenses { get; set; }
        public VendorListing Vendor { get; set; }
        public string InvoiceType { get; set; }

        public DateTime InvoiceDate { get; set; }
        public string Description { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        public static InvoiceDetails MapFromObject(Invoice invoice)
        {
            var details = new InvoiceDetails()
            {
                InvoiceId = invoice.InvoiceId,
                InvoiceDate = invoice.InvoiceDate,
                InvoiceNumber = invoice.InvoiceNumber,
                InvoiceType = invoice.InvoiceType.Name,
                Description = invoice.Description
            };

            details.Expenses = new List<InvoiceExpense>();
            foreach (var accountTotal in invoice.AccountTotals)
            {

                var account = new AccountListing()
                {
                    AccountId = accountTotal.Account.AccountId,
                    AccountNumber = accountTotal.Account.AccountNumber,
                    SubNo = accountTotal.Account.SubNo,
                    ShredNo = accountTotal.Account.ShredNo

                };

                details.Expenses.Add(new InvoiceExpense()
                {
                    Expense = accountTotal.Expense,
                    Account = account
                });
            }

            details.Vendor = new VendorListing()
            {
                VendorId = invoice.Vendor.VendorId,
                Name = invoice.Vendor.Name
            };

            return details;
        }

    }
}
