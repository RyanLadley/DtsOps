using dtso.core.Managers.Interfaces;
using dtso.core.Services;
using dtso.data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace dtso.core.Managers
{
    public class InvoiceManager : IInvoiceManager
    {
        private IInvoiceRepository _invoiceRepository;

        public InvoiceManager(IInvoiceRepository invoiceRepo)
        {
            _invoiceRepository = invoiceRepo;
        }

        public Invoice GetInvoice(int invoiceId)
        {
            return Invoice.MapFromEntity(_invoiceRepository.Get(invoiceId));
        }

        /// <summary>
        /// Retrieves the list of all Invoices that fall under the provided account.
        /// </summary>
        public List<Invoice> GetInvoicesForAccount(AccountNumberTemplate accountNumber)
        {
            var invoices = new List<Invoice>();
            foreach(var invoiceEntity in _invoiceRepository.GetInvoicesForAccount(accountNumber.AccountNumber.Value, accountNumber.SubNo, accountNumber.ShredNo))
            {
                var invoice = Invoice.MapFromEntity(invoiceEntity);

                //Remove AccountTotals/Expenses associated with this invoice that don't match the mapping of the provided account number
                invoice.AccountTotals.RemoveAll(accountTotal => !_IsMatchingAccount(accountTotal.Account, accountNumber));
                invoices.Add(invoice);
            }

            return invoices;
        }
        

        private bool _IsMatchingAccount(Account account, AccountNumberTemplate accountNumber)
        {
            if (accountNumber.ShredNo.HasValue)
                return account.ShredNo == accountNumber.ShredNo && account.SubNo == accountNumber.SubNo && account.AccountNumber == accountNumber.AccountNumber;

            else if (accountNumber.SubNo.HasValue)
                return account.SubNo == accountNumber.SubNo && account.AccountNumber == accountNumber.AccountNumber;

            else if (accountNumber.AccountNumber.HasValue)
                return account.AccountNumber == accountNumber.AccountNumber;

            return false;
        }
    }
}
