using dtso.core.Enums;
using dtso.core.Managers.Interfaces;
using dtso.core.Models;
using dtso.core.Utilties;
using dtso.data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace dtso.core.Managers
{
    public class InvoiceManager : IInvoiceManager
    {
        private IInvoiceRepository _invoiceRepository;
        private TicketManager _ticketManager;

        public InvoiceManager(IInvoiceRepository invoiceRepo, TicketManager ticketMan)
        {
            _invoiceRepository = invoiceRepo;
            _ticketManager = ticketMan;
        }

        public Invoice GetInvoice(int invoiceId)
        {
            var invoice = Invoice.MapFromEntity(_invoiceRepository.Get(invoiceId));
            invoice.Tickets = _ticketManager.GetTicketsForInvoice(invoice.InvoiceId);

            return invoice;
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

        public int CreateInvoice(Invoice invoice, ref Error error)
        {
            _validateInvoiceForm(invoice, ref error);

            if (error.ErrorCode != ErrorCode.OKAY)
                return -1;

            var invoiceId = _invoiceRepository.Add(invoice.MapToEntity());

            return invoiceId;

        }

        public Invoice EditInvoice(Invoice invoice, ref Error error)
        {
            _validateInvoiceForm(invoice, ref error);

            if (error.ErrorCode != ErrorCode.OKAY)
                return null;

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

        private void _validateInvoiceForm(Invoice invoice, ref Error error)
        {
            if (string.IsNullOrEmpty(invoice.InvoiceNumber))
            {
                error.ErrorCode = ErrorCode.INVALID;
                error.Message = "An Invoice Number Must Be Provided";
            }
            else if(invoice.InvoiceDate.Year != 2018)
            {
                error.ErrorCode = ErrorCode.INVALID;
                error.Message = "An Invoice Date Must In The Year 2018";
            }
            else if (invoice.DatePaid.Year != 2018)
            {
                error.ErrorCode = ErrorCode.INVALID;
                error.Message = "The Date Paid Must Be Provided";
            }
            else if (invoice.InvoiceType.InvoiceTypeId == 0)
            {
                error.ErrorCode = ErrorCode.INVALID;
                error.Message = "An Invoice Type Must Be Provided";
            }
            else if (invoice.Vendor.VendorId == 0)
            {
                error.ErrorCode = ErrorCode.INVALID;
                error.Message = "A Vendor Must Be Provided";
            }
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

        public Invoice AddTicketsToInvoice(InvoiceTickets invoiceTickets)
        {
            _invoiceRepository.AddTicketsToInvoice(invoiceTickets.InvoiceId, invoiceTickets.TicketIds);

            return GetInvoice(invoiceTickets.InvoiceId);
        }

        public void RemoveCityExpensesFromInvoice(List<int> CityExpensesToRemove)
        {
            _invoiceRepository.RemoveCityExpense(CityExpensesToRemove);
            
        }

        public void RemoveInvoiceAccounts(List<int> invoiceAccountsToRemove)
        {
            _invoiceRepository.RemoveInvoiceAccounts(invoiceAccountsToRemove);
        }
    }
}
