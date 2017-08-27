using dtso.data.Entities;
using System.Collections.Generic;

namespace dtso.data.Repositories
{
    public interface IInvoiceRepository
    {
        Invoice Add(Invoice invoice);
        Invoice Get(int id);
        List<Invoice> GetInvoicesForAccount(int AccountNumber, int? SubNo, int? ShredNo);
        void Remove(int id);
        Invoice Update(Invoice invoice);
    }
}