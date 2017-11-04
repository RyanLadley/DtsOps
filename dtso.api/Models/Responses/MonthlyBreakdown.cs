using dtso.api.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dtso.api.Models.Responses
{
    public class MonthlyBreakdown
    {
        public int Month { get; set; }
        public List<InvoiceBasic> Invoices { get; set; }
        public List<TicketBasic> Tickets { get; set; }
        public decimal TotalExpense { get; set; }

        public static MonthlyBreakdown MapFromObject(core.Models.MonthlyBreakdown obj, ResponseGenerator responseGenerator)
        {
            return new MonthlyBreakdown()
            {
                Month = obj.Month,
                TotalExpense = obj.TotalExpense,
                Invoices = responseGenerator.GenerateBasicInvoicesList(obj.Invoices),
                Tickets = responseGenerator.GenerateBasicTicketList(obj.Tickets)
            };
        }
    }
}
