using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dtso.core.Services;
using dtso.api.Models.Responses;

namespace dtso.api.Utilities
{
    public class ResponseGenerator
    {
        public List<InvoiceBasic> GenerateBasicInvoices(Invoice invoice)
        {
            List<InvoiceBasic> invoiceResponses = new List<InvoiceBasic>();
            foreach(var accountTotal in invoice.AccountTotals)
            {
                invoiceResponses.Add(InvoiceBasic.MapFromObject(invoice, new AccountNumberTemplate(accountTotal.Account).stringifyAccountNumber(), accountTotal.Expense));
            }

            return invoiceResponses;
        }


        

    }
}
