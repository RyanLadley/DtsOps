using System;
using System.Collections.Generic;
using System.Text;
using dtso.data.Entities;
using dtso.data.Context;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace dtso.data.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private MainContext _context;

        public InvoiceRepository(MainContext context)
        {
            _context = context;

        }
        public Invoice Add(Invoice account)
        {
            throw new NotImplementedException();
        }

        public Invoice Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Invoice> GetInvoicesForAccount(int AccountNumber, int? SubNo, int? ShredNo)
        {
            if (ShredNo.HasValue && SubNo.HasValue)
            {
                return _getShredInvoices(AccountNumber, SubNo.Value, ShredNo.Value);
            }
            else if (SubNo.HasValue)
            {
                return _getSubInvoices(AccountNumber, SubNo.Value);
            }
            else
            {
                return _getRootInvoices(AccountNumber);
            }
        }

        private List<Invoice> _getRootInvoices(int accountNumber)
        {
            var invoices =
                (from invoice in _context.Invoices
                 join invoiceAccount in _context.InvoiceAccounts on invoice.InvoiceId equals invoiceAccount.InvoiceId
                 join vAccount in _context.vAccounts on invoiceAccount.AccountId equals vAccount.AccountId
                 where vAccount.AccountNumber == accountNumber
                 select invoice).Distinct()

                .Include(invoice => invoice.InvoiceAccounts)
                    .ThenInclude(ia => ia.vAccount)
                .Include(invoice => invoice.InvoiceType)
                .Include(invoice => invoice.Vendor);

            return invoices.ToList();
        }

        private List<Invoice> _getSubInvoices(int accountNumber, int SubNo)
        {
            var invoices =
                (from invoice in _context.Invoices
                 join invoiceAccount in _context.InvoiceAccounts on invoice.InvoiceId equals invoiceAccount.InvoiceId
                 join vAccount in _context.vAccounts on invoiceAccount.AccountId equals vAccount.AccountId
                 where vAccount.AccountNumber == accountNumber
                    && vAccount.SubNo == SubNo
                 select invoice)

                .Include(invoice => invoice.InvoiceAccounts)
                    .ThenInclude(ia => ia.vAccount)
                .Include(invoice => invoice.Vendor);

            return invoices.ToList();
        }

        private List<Invoice> _getShredInvoices(int accountNumber, int SubNo, int ShredNo)
        {
            var invoices =
                (from invoice in _context.Invoices
                 join invoiceAccount in _context.InvoiceAccounts on invoice.InvoiceId equals invoiceAccount.InvoiceId
                 join vAccount in _context.vAccounts on invoiceAccount.AccountId equals vAccount.AccountId
                 where vAccount.AccountNumber == accountNumber
                    && vAccount.SubNo == SubNo
                    && vAccount.ShredNo == ShredNo
                 select invoice)

                .Include(invoice => invoice.InvoiceAccounts)
                    .ThenInclude(ia => ia.vAccount)
                .Include(invoice => invoice.Vendor);

            return invoices.ToList(); 
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Invoice Update(Invoice invoice)
        {
            throw new NotImplementedException();
        }
    }
}
