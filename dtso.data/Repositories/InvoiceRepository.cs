using System;
using System.Collections.Generic;
using System.Text;
using dtso.data.Entities;
using dtso.data.Context;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using dtso.data.Repositories.Interfaces;
using System.Data.SqlClient;

namespace dtso.data.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private MainContext _context;

        string sqlRemoveTicketsFromInvoice =
            "UPDATE Tickets " +
            "   SET InvoiceId = NULL " +
            "   WHERE InvoiceId = @invoiceId ";

        string sqlAddTicketToInvoice =
            "UPDATE Tickets " +
            "   SET InvoiceId = @invoiceId " +
            "   WHERE TicketId = @ticketId ";

        public InvoiceRepository(MainContext context)
        {
            _context = context;

        }
        public int Add(Invoice invoice)
        {
            _context.Invoices.Add(invoice);
            _context.SaveChanges();

            return invoice.InvoiceId;
        }

        public Invoice Get(int id)
        {
            var singleInvoice = _context.Invoices
                .Where(invoice => invoice.InvoiceId == id)

                .Include(invoice => invoice.Vendor)
                .Include(invoice => invoice.InvoiceType)
                .Include(invoice => invoice.InvoiceAccounts)
                    .ThenInclude(ia => ia.vAccount)
                .Include(invoice => invoice.InvoiceAccounts)
                    .ThenInclude(ia => ia.CityExpenses)
                        .ThenInclude(cityExpense => cityExpense.CityAccount);

            //Tickets are added seperatly due to the complexity of the entity

            return singleInvoice.FirstOrDefault();
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
                .Include(invoice => invoice.InvoiceAccounts)
                    .ThenInclude(ia => ia.CityExpenses)
                .Include(invoice => invoice.Vendor);

            return invoices.ToList(); 
        }

        public List<Invoice> GetInvoicesForVendor(int vendorId)
        {
            return _context.Invoices
                .Where(invoice => invoice.VendorId == vendorId)

                .Include(invoice => invoice.Vendor)
                .Include(invoice => invoice.InvoiceType)
                .Include(invoice => invoice.InvoiceAccounts)
                    .ThenInclude(ia => ia.vAccount)
                .ToList();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public int Update(Invoice invoice)
        {
            _context.Invoices.Update(invoice);
            _context.SaveChanges();

            return invoice.InvoiceId;
        }

        public List<InvoiceType> GetTypes()
        {
            return _context.InvoiceTypes.ToList();
        }

        public int Add(InvoiceAccount invoiceAccount)
        {
            _context.InvoiceAccounts.Add(invoiceAccount);
            _context.SaveChanges();

            return invoiceAccount.InvoiceAccountId;
        }

        public int Add(CityExpense cityExpense)
        {
            _context.CityExpenses.Add(cityExpense);
            _context.SaveChanges();

            return cityExpense.CityExpenseId;
        }

        public int Update(InvoiceAccount invoiceAccount)
        {
            _context.InvoiceAccounts.Update(invoiceAccount);
            _context.SaveChanges();

            return invoiceAccount.InvoiceAccountId;
        }

        public int Update(CityExpense cityExpense)
        {
            _context.CityExpenses.Update(cityExpense);
            _context.SaveChanges();

            return cityExpense.CityExpenseId;
        }

        public int AddTicketsToInvoice(int invoiceId, int[] ticketIds)
        {
            var paramInvoiceId = new SqlParameter("@invoiceId", invoiceId);
            _context.Database.ExecuteSqlCommand(sqlRemoveTicketsFromInvoice, paramInvoiceId);

            foreach(var ticketId in ticketIds)
            {
                var paramTicketId = new SqlParameter("@ticketId", ticketId);
                _context.Database.ExecuteSqlCommand(sqlAddTicketToInvoice, paramInvoiceId, paramTicketId);
            }

            return invoiceId;
        }

        public void RemoveCityExpense(List<int> CityExpensesToRemove)
        {
            foreach(var cityExpenseId in CityExpensesToRemove)
            {
                var paramCityExpenseId = new SqlParameter("@cityExpenseId", cityExpenseId);
                _context.Database.ExecuteSqlCommand("DELETE CityExpenses WHERE CityExpenseId = @cityExpenseId", paramCityExpenseId);
            }
        }

        public void RemoveInvoiceAccounts(List<int> invoiceAccountsToRemove)
        {
            foreach (var invoiceAccountId in invoiceAccountsToRemove)
            {
                var paramInvoiceAccountId = new SqlParameter("@invoiceAccountId", invoiceAccountId);
                _context.Database.ExecuteSqlCommand("DELETE CityExpenses WHERE InvoiceAccountId = @invoiceAccountId", paramInvoiceAccountId);
                _context.Database.ExecuteSqlCommand("DELETE InvoiceAccounts WHERE InvoiceAccountId = @invoiceAccountId", paramInvoiceAccountId);
            }
        }
    }
}
