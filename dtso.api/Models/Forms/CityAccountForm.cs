using dtso.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dtso.api.Models.Forms
{
    public class CityAccountForm
    {
        public int CityAccountId { get; set; }
        public int? CityExpenseId { get; set; }
        public decimal Expense { get; set; }
        
        public CityExpense MapToCore(int? invoiceAccountId)
        {
            var cityExpense = new CityExpense()
            {
                InvoiceAccountId = (invoiceAccountId.HasValue) ? invoiceAccountId.Value : 0,
                CityAccount = new CityAccount() { CityAccountId = this.CityAccountId },
                CityExpenseId = (CityExpenseId.HasValue) ? this.CityExpenseId.Value : 0,
                Expense = this.Expense
            };

            return cityExpense;
        }
    }
}
