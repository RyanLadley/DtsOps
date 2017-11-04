using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dtso.api.Models.Responses
{
    public class InvoiceExpense
    {
        public int InvoiceAccountId { get; set; }
        public AccountListing Account { get; set; }
        public decimal Expense { get; set; }
        public List<CityExpenseBasic> CityExpense { get; set; }


    }
}
