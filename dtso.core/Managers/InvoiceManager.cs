using dtso.core.Managers.Interfaces;
using dtso.core.Models;
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

        /// <summary>
        /// Retrieves the list of all Invoices that are for the provided vendor.
        /// </summary>
        public List<Invoice> GetInvoicesForVendor(int vendorId)
        {
            var invoices = new List<Invoice>();
            foreach (var invoiceEntity in _invoiceRepository.GetInvoicesForVendor(vendorId))
            {
                var invoice = Invoice.MapFromEntity(invoiceEntity);
                invoices.Add(invoice);
            }

            return invoices;
        }

        public string CreateInvoice(Invoice invoice)
        {
            //Add Validation Here
            //Add Serverside Validation Here
            if (_invoiceRepository.Add(invoice.MapToEntity()))
            {
                return "SUCCESS";
            }
            else
            {
                return "There was an error adding the invoice.";
            }

        }

        public Invoice EditInvoice(Invoice invoice)
        {
            //Add Validation Here
            //Add Serverside Validation Here

            //Update Exisint invoice  accounts and city epenses
            var newInvoiceAccounts = new List<InvoiceAccountTotal>();
            foreach(var invoiceAccount in invoice.AccountTotals)
            {
                //Add new Invoice Account to list to be attacthed to INvoice. They will be Added then. All information here is new, so no need for silly loops
                if(invoiceAccount.InvoiceAccountId <= 0)
                    newInvoiceAccounts.Add(invoiceAccount);

                else
                {
                    //Update or Add City Expnses for the invoice acount
                    foreach(var cityExpense in invoiceAccount.CityExpenses)
                    {
                        if (cityExpense.CityExpenseId <= 0)
                            _invoiceRepository.Add(cityExpense.MapToEntity());
                        else
                            _invoiceRepository.Update(cityExpense.MapToEntity());
                    }

                    //Update the invoice account itself
                    invoiceAccount.CityExpenses = new List<CityExpense>() ;
                    _invoiceRepository.Update(invoiceAccount.MapToEntity(invoice.InvoiceId));
                }
            }

            invoice.AccountTotals = newInvoiceAccounts;
            //Fincally, update the invoice uinformation
            var invoiceId = _invoiceRepository.Update(invoice.MapToEntity());
            
            return GetInvoice(invoiceId);

        }


        public List<InvoiceType> GetInvoiceTypes()
        {
            var types = new List<InvoiceType>();

            foreach(var type in _invoiceRepository.GetTypes())
            {
                types.Add(InvoiceType.MapFromEntity(type));
            }

            return types;
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
