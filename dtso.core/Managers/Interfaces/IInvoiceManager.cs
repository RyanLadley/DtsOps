using dtso.core.Models;
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
        string CreateInvoice(Invoice invoice);
        Invoice EditInvoice(Invoice invoice);
    }
}
