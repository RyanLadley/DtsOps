using dtso.core.Models;
using dtso.data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace dtso.core.Managers
{
    public class TicketManager
    {
        ITicketRepository _ticketRepository;
        public TicketManager(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public int AddTicket(Ticket ticket)
        {
            var ticketId = _ticketRepository.Add(ticket.MapToEntity());

            return ticketId;
        }

        public List<Ticket> GetTicketsForVendor(int vendorId, bool onlyPending = false)
        {
            var tickets = new List<Ticket>();
            foreach(var ticket in _ticketRepository.GetForVendor(vendorId, onlyPending))
            {
                tickets.Add(Ticket.MapFromEntity(ticket));
            }

            return tickets;
        }

        public List<Ticket> GetTicketsForAccount(AccountNumberTemplate accountNumber, bool onlyPending = false)
        {
            var tickets = new List<Ticket>();
            foreach (var ticket in _ticketRepository.GetTicketsForAccount(accountNumber.AccountNumber.Value, accountNumber.SubNo, accountNumber.ShredNo, onlyPending))
            {
                tickets.Add(Ticket.MapFromEntity(ticket));
            }

            return tickets;
        }

        public Ticket EditTicket(Ticket ticket)
        {
            var ticketId = _ticketRepository.Update(ticket.MapToEntity());

            return GetTicket(ticketId);
        }

        public List<Ticket> GetTicketsForInvoice(int invoiceId)
        {
            var tickets = new List<Ticket>();
            foreach (var ticket in _ticketRepository.GetTicketsForInvoice(invoiceId))
            {
                tickets.Add(Ticket.MapFromEntity(ticket));
            }

            return tickets;
        }

        public Ticket GetTicket(int ticketId)
        {
            var ticket = Ticket.MapFromEntity(_ticketRepository.GetTicket(ticketId));

            return ticket;
        }
    }
}
