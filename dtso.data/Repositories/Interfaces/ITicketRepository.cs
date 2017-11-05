using dtso.data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace dtso.data.Repositories.Interfaces
{
    public interface ITicketRepository
    {
        int Add(Ticket ticket);
        List<Ticket> GetForVendor(int vendorId, bool onlyPending);
        List<Ticket> GetTicketsForAccount(int AccountNumber, int? SubNo, int? ShredNo, bool onlyPending);
        List<Ticket> GetTicketsForInvoice(int invoiceId);
    }
}
