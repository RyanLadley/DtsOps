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
        public decimal TotalExpense { get; set; }

        public static MonthlyBreakdown MapFromObject(core.Services.MonthlyBreakdown obj, ResponseGenerator responseGenerator)
        {
            var breakdown = new MonthlyBreakdown()
            {
                Month = obj.Month,
                TotalExpense = obj.TotalExpense
            };

            var invoices = new List<InvoiceBasic>();
            foreach(var invoice in obj.Invoices)
            {
                foreach(var mappedinvoice in responseGenerator.GenerateBasicInvoices(invoice))
                {
                    invoices.Add(mappedinvoice);
                }
            }
            breakdown.Invoices = invoices;

            return breakdown;
        }
    }
}
