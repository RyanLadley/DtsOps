using System;
using System.Collections.Generic;
using System.Text;

namespace dtso.core.Models
{
    public class CityExpense
    {

        public int InvoiceAccountId { get; set; }
        public int CityExpenseId { get; set; }
        public CityAccount CityAccount { get; set; }
        public decimal Expense { get; set; }

        public data.Entities.CityExpense MapToEntity()
        {
            var entity = new data.Entities.CityExpense()
            {
                CityAccountId = this.CityAccount.CityAccountId,
                InvoiceAccountId = this.InvoiceAccountId,
                CityExpenseId = this.CityExpenseId,
                Expense = this.Expense
            };

            return entity;
        }

        public static CityExpense MapFromEntity(data.Entities.CityExpense entity)
        {
            if (entity == null)
                return null;

            return new CityExpense()
            {
                InvoiceAccountId = entity.InvoiceAccountId,
                CityExpenseId = entity.CityExpenseId,
                CityAccount = CityAccount.MapFromEntity(entity.CityAccount),
                Expense = entity.Expense
            };
            
        }
    }
}
