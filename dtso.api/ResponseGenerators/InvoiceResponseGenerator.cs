using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dtso.core.Services;
using dtso.api.Models.Responses;

namespace dtso.api.ResponseGenerators
{
    public class InvoiceResponseGenerator
    {
        public List<InvoiceBasic> GenerateBasicInvoicesForAccount(Invoice invoice, AccountNumberBreakdown accountNumber)
        {
            List<InvoiceBasic> invoiceResponses = new List<InvoiceBasic>();

            foreach(var accountTotal in invoice.AccountTotals)
            {
                if(_IsMatchingAccount(accountTotal.Account, accountNumber))
                {
                    invoiceResponses.Add(_generateBasicInvoice(invoice, new AccountNumberBreakdown(accountTotal.Account).stringifyAccountNumber(), accountTotal.Expense));
                }

            }

            return invoiceResponses;
        }
        
        private bool _IsMatchingAccount(Account account, AccountNumberBreakdown accountNumber)
        {
            if (accountNumber.ShredNo.HasValue)
                return account.ShredNo == accountNumber.ShredNo && account.SubNo == accountNumber.SubNo && account.AccountNumber == accountNumber.AccountNumber;
            
            else if (accountNumber.SubNo.HasValue)
                return account.SubNo == accountNumber.SubNo && account.AccountNumber == accountNumber.AccountNumber;
            
            else if (accountNumber.AccountNumber.HasValue)
                return account.AccountNumber == accountNumber.AccountNumber;

            return false;
        }

        /// <summary>
        /// Flattens the given invoice into a "Basic Invoice" Reposne using the ginven account number and expense for the account
        /// </summary>
        private InvoiceBasic _generateBasicInvoice(Invoice invoice, string accountNumber, decimal expense)
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

            var vendor = new VendorListing()
            {
                VendorId = invoice.Vendor.VendorId,
                Name = invoice.Vendor.Name
            };

            basicInvoice.Vendor = vendor;

            return basicInvoice;
        }
    }
}
