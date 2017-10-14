using System;
using System.Collections.Generic;
using System.Text;

namespace dtso.core.Services
{
    public class MonthlyBreakdown
    {
        public int Month { get; set; }
        public List<Invoice> Invoices { get; set; }
        public decimal TotalExpense { get; set; }

        public MonthlyBreakdown(int month)
        {
            Month = month;
            TotalExpense = 0;
            Invoices = new List<Invoice>();
        }
    }
}
