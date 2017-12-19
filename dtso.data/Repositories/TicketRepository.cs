using dtso.data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using dtso.data.Entities;
using dtso.data.Context;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace dtso.data.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        MainContext _context;
        public TicketRepository(MainContext context)
        {
            _context = context;
        }

        public int Add(Ticket ticket)
        {
            _context.Tickets.Add(ticket);
            _context.SaveChanges();
            return ticket.TicketId;
        }

        public List<Ticket> GetForVendor(int vendorId, bool onlyPending)
        {
            return _context.Tickets
                .Where(ticket => ticket.VendorId == vendorId && (!onlyPending || (onlyPending && ticket.InvoiceId == null)))

                .Include(ticket => ticket.vAccount)
                .Include(ticket => ticket.Invoice)
                .Include(ticket => ticket.Vendor)
                .Include(ticket => ticket.MaterialVendor)
                    .ThenInclude(materialVendor => materialVendor.Material)
                .ToList();
        }

        public List<Ticket> GetTicketsForInvoice(int invoiceId)
        {
            var tickets =_context.Tickets
                        .Where(ticket => ticket.InvoiceId == invoiceId)

                    .Include(ticket => ticket.vAccount)
                    .Include(ticket => ticket.Invoice)
                    .Include(ticket => ticket.Vendor)
                    .Include(ticket => ticket.MaterialVendor)
                        .ThenInclude(materialVendor => materialVendor.Material);

            return tickets.ToList();
        }

        public List<Ticket> GetTicketsForAccount(int AccountNumber, int? SubNo, int? ShredNo, bool onlyPending)
        {
            if (ShredNo.HasValue && SubNo.HasValue)
            {
                return _getShredTickets(AccountNumber, SubNo.Value, ShredNo.Value, onlyPending);
            }
            else if (SubNo.HasValue)
            {
                return _getSubTickets(AccountNumber, SubNo.Value, onlyPending);
            }
            else
            {
                return _getRootTickets(AccountNumber, onlyPending);
            }
        }

        private List<Ticket> _getRootTickets(int accountNumber, bool onlyPending)
        {
            var tickets =
                (from ticket in _context.Tickets
                 join vAccount in _context.vAccounts on ticket.AccountId equals vAccount.AccountId
                 where vAccount.AccountNumber == accountNumber 
                    && (!onlyPending || (onlyPending && ticket.InvoiceId == null))
                 select ticket)

                .Include(ticket => ticket.vAccount)
                .Include(ticket => ticket.Invoice)
                .Include(ticket => ticket.Vendor)
                .Include(ticket => ticket.MaterialVendor)
                    .ThenInclude(materialVendor => materialVendor.Material);

            return tickets.ToList();
        }

        private List<Ticket> _getSubTickets(int accountNumber, int SubNo, bool onlyPending)
        {
            var tickets =
                (from ticket in _context.Tickets
                 join vAccount in _context.vAccounts on ticket.AccountId equals vAccount.AccountId
                 where vAccount.AccountNumber == accountNumber
                    && vAccount.SubNo == SubNo
                    && (!onlyPending || (onlyPending && ticket.InvoiceId == null))
                 select ticket)

                .Include(ticket => ticket.vAccount)
                .Include(ticket => ticket.Invoice)
                .Include(ticket => ticket.Vendor)
                .Include(ticket => ticket.MaterialVendor)
                    .ThenInclude(materialVendor => materialVendor.Material);

            return tickets.ToList();
        }

        private List<Ticket> _getShredTickets(int accountNumber, int SubNo, int ShredNo, bool onlyPending)
        {
            var tickets =
                (from ticket in _context.Tickets
                 join vAccount in _context.vAccounts on ticket.AccountId equals vAccount.AccountId
                 where vAccount.AccountNumber == accountNumber
                    && vAccount.SubNo == SubNo
                    && vAccount.ShredNo == ShredNo
                    && (!onlyPending || (onlyPending && ticket.InvoiceId == null))
                 select ticket)

                .Include(ticket => ticket.vAccount)
                .Include(ticket => ticket.Invoice)
                .Include(ticket => ticket.Vendor)
                .Include(ticket => ticket.MaterialVendor)
                    .ThenInclude(materialVendor => materialVendor.Material);

            return tickets.ToList();
        }

        public Ticket GetTicket(int ticketId)
        {
            return _context.Tickets
                .Where(ticket => ticket.TicketId == ticketId)

                .Include(ticket => ticket.vAccount)
                .Include(ticket => ticket.Invoice)
                .Include(ticket => ticket.Vendor)
                .Include(ticket => ticket.MaterialVendor)
                    .ThenInclude(materialVendor => materialVendor.Material)
                .FirstOrDefault();
        }

        public int Update(Ticket ticket)
        {
            _context.Tickets.Update(ticket);
            _context.SaveChanges();

            return ticket.TicketId;
        }
    }
}
