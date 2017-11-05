using dtso.data.Entities;
using System.Collections.Generic;

namespace dtso.data.Repositories.Interfaces
{
    public interface IInvoiceRepository
    {
        int Add(Invoice invoice);
        int Add(InvoiceAccount invoiceAccount);
        int Add(CityExpense cityExpense);
        Invoice Get(int id);
        List<Invoice> GetInvoicesForAccount(int AccountNumber, int? SubNo, int? ShredNo);
        List<Invoice> GetInvoicesForVendor(int vendorId);
        void Remove(int id);
        int Update(Invoice invoice);
        int Update(InvoiceAccount invoiceAccount);
        int Update(CityExpense cityExpense);
        List<InvoiceType> GetTypes();
        int AddTicketsToInvoice(int invoiceId, int[] ticketIds);
        void RemoveCityExpense(List<int> cityExpensesToRemove);
        void RemoveInvoiceAccounts(List<int> invoiceAccountsToRemove);
    }
}