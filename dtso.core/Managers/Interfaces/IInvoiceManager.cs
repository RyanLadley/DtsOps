using dtso.core.Services;
using System.Collections.Generic;
using System.Text;

namespace dtso.core.Managers.Interfaces
{
    public interface IInvoiceManager
    {
        List<Invoice> GetInvoicesForAccount(AccountNumberBreakdown accountNumber);
    }
}
