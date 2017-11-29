using dtso.core.Enums;
using dtso.core.Models;
using dtso.core.Utilties;
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

        public void AddTickets(List<Ticket> tickets, ref Error error)
        {
            //First Validate All tickets
            foreach(var ticket in tickets)
            {
                _validateTicket(ticket, ref error);

                if (error.ErrorCode != ErrorCode.OKAY)
                    return;
            }

            //If All Tiktes Are Valid, add them
            foreach (var ticket in tickets)
            {
                var ticketId = _ticketRepository.Add(ticket.MapToEntity());
            }
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

        public Ticket EditTicket(Ticket ticket, ref Error error)
        {
            _validateTicket(ticket, ref error);

            if (error.ErrorCode != ErrorCode.OKAY)
                return null;

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

        private void _validateTicket(Ticket ticket, ref Error error)
        {
            if (ticket.VendorId == 0)
            {
                error.ErrorCode = ErrorCode.INVALID;
                error.Message = "A Vendor Must Be Selected";
            }
            else if (ticket.AccountId == 0)
            {
                error.ErrorCode = ErrorCode.INVALID;
                error.Message = "An Account Must Be Selected";
            }
            else if (ticket.Date.Year < 2000)
            {
                error.ErrorCode = ErrorCode.INVALID;
                error.Message = "A Valid Date Must Be Provided";
            }
            else if (string.IsNullOrEmpty(ticket.TicketNumber))
            {
                error.ErrorCode = ErrorCode.INVALID;
                error.Message = "A Ticket Number Must Be Provided";
            }
            else if (string.IsNullOrEmpty(ticket.TicketNumber))
            {
                error.ErrorCode = ErrorCode.INVALID;
                error.Message = "An Ticket Number Must Be Provided";
            }
            else if(ticket.Quantity <= 0)
            {
                error.ErrorCode = ErrorCode.INVALID;
                error.Message = "A Valid (Poisitve) quantity must be provided";
            }
            else if (ticket.MaterialVendorId <= 0)
            {
                error.ErrorCode = ErrorCode.INVALID;
                error.Message = "A Material Must Be Selected";
            }
        }
    }
}
