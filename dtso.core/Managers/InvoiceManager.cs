using dtso.core.Managers.Interfaces;
using dtso.core.Services;
using dtso.data.Repositories;
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

        public List<Invoice> GetInvoicesForAccount(AccountNumberBreakdown accountNumber)
        {
            var invoices = new List<Invoice>();
            foreach(var invoiceEntity in _invoiceRepository.GetInvoicesForAccount(accountNumber.AccountNumber.Value, accountNumber.SubNo, accountNumber.ShredNo))
            {
                invoices.Add(Invoice.MapFromEntity(invoiceEntity));
            }
            return invoices;
        }
    }
}
