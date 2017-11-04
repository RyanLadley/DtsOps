using System;
using System.Collections.Generic;
using System.Text;

namespace dtso.data.Entities
{
    public class CityExpense
    {
        public int CityExpenseId { get; set; }
        public int InvoiceAccountId { get; set; }
        public int CityAccountId { get; set; }
        public decimal Expense { get; set; }
        
        public CityAccount CityAccount { get; set; }
        public InvoiceAccount InvoiceAccount { get; set; }
    }
}
