using dtso.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dtso.api.Models.Forms
{
    public class InvoiceTicketForm
    {
        public int InvoiceId { get; set; }
        public int[] TicketIds { get; set; }

        public InvoiceTickets MapToCore()
        {
            return new InvoiceTickets()
            {
                InvoiceId = this.InvoiceId,
                TicketIds = this.TicketIds
            };
        }
    }
}
