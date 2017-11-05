using dtso.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dtso.api.Models.Forms
{
    public class InvoiceAccountForm
    {
        public int? InvoiceAccountId { get; set; }
        public int AccountId { get; set; }
        public decimal Expense { get; set; }
        public List<CityAccountForm> CityAccounts { get; set; }

        public InvoiceAccountTotal MapToCore()
        {
            var invoiceAcount = new InvoiceAccountTotal()
            {
                InvoiceAccountId = (InvoiceAccountId.HasValue) ? InvoiceAccountId.Value : 0,
                Account = new Account() { AccountId = this.AccountId },
                Expense = this.Expense
            };

            invoiceAcount.CityExpenses = new List<CityExpense>();
            foreach(var account in CityAccounts)
            {
                if(account.CityAccountId > 0)
                    invoiceAcount.CityExpenses.Add(account.MapToCore(InvoiceAccountId));
            }

            return invoiceAcount;
        }
    }
}
