using dtso.core.Models;
using dtso.core.Utilties;
using System.Collections.Generic;
using System.Text;

namespace dtso.core.Managers.Interfaces
{
    public interface IInvoiceManager
    {
        List<Invoice> GetInvoicesForAccount(AccountNumberTemplate accountNumber);
        List<Invoice> GetInvoicesForVendor(int vendorId);
        Invoice GetInvoice(int invoiceId);
        List<InvoiceType> GetInvoiceTypes();
        int CreateInvoice(Invoice invoice, ref Error error);
        Invoice EditInvoice(Invoice invoice, ref Error error);
        Invoice AddTicketsToInvoice(InvoiceTickets invoiceTickets);
        void RemoveCityExpensesFromInvoice(List<int> CityExpensesToRemove);
        void RemoveInvoiceAccounts(List<int> invoiceAccountsToRemove);
    }
}
