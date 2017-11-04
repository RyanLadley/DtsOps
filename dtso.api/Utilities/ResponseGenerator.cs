using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dtso.core.Models;
using dtso.api.Models.Responses;

namespace dtso.api.Utilities
{
    public class ResponseGenerator
    {
        public List<InvoiceBasic> GenerateBasicInvoices(Invoice invoice)
        {
            List<InvoiceBasic> invoiceResponses = new List<InvoiceBasic>();
            foreach(var accountTotal in invoice.AccountTotals)
            {
                invoiceResponses.Add(InvoiceBasic.MapFromObject(invoice, new AccountNumberTemplate(accountTotal.Account).stringifyAccountNumber(), accountTotal.Expense));
            }

            return invoiceResponses;
        }

        public List<InvoiceBasic> GenerateBasicInvoicesList(List<Invoice> invoices)
        {
            var basicInvoices = new List<InvoiceBasic>();
            foreach (var invoice in invoices)
            {
                foreach (var mappedinvoice in this.GenerateBasicInvoices(invoice))
                {
                    basicInvoices.Add(mappedinvoice);
                }
            }
            return basicInvoices;
        }

        public List<TicketBasic> GenerateBasicTicketList(List<Ticket> tickets)
        {
            var basicTickets = new List<TicketBasic>();
            foreach(var ticket in tickets)
            {
                basicTickets.Add(TicketBasic.MapFromObject(ticket));
            }

            return basicTickets;
        }
    }
}
