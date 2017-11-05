using System;
using System.Collections.Generic;
using System.Text;

namespace dtso.core.Models
{
    public class InvoiceTickets
    {
        public int InvoiceId { get; set; }
        public int[] TicketIds { get; set; }
    }
}
