using dtso.data.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace dtso.data.Entities
{
    public class InvoiceAccount
    {
        public int InvoiceAccountId { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public vAccount vAccount { get; set; }

        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }

        public decimal Expense { get; set; }

        public List<CityExpense> CityExpenses { get; set; }
    }
}
