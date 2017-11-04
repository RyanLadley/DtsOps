using System;
using System.Collections.Generic;
using System.Text;

namespace dtso.core.Models
{
    public class InvoiceAccountTotal
    {
        public int InvoiceAccountId { get; set; }
        public Account Account { get; set; }
        public decimal Expense { get; set; }
        public List<CityExpense> CityExpenses { get; set; }

        public static InvoiceAccountTotal MapFromEntity(data.Entities.InvoiceAccount entity)
        {
            List<CityExpense> cityExpenses = null;
            if (entity.CityExpenses != null)
            {
                cityExpenses = new List<CityExpense>();
                foreach (var expense in entity.CityExpenses)
                {
                    cityExpenses.Add(CityExpense.MapFromEntity(expense));
                }
            }

            return new InvoiceAccountTotal()
            {

                InvoiceAccountId = entity.InvoiceAccountId,
                Account = Account.MapFromObject(entity.vAccount),
                CityExpenses = cityExpenses ?? null,
                Expense = entity.Expense
            };
        }

        public data.Entities.InvoiceAccount MapToEntity(int invoiceId)
        {
            var entity = new data.Entities.InvoiceAccount()
            {
                InvoiceId = invoiceId,
                InvoiceAccountId = this.InvoiceAccountId,
                AccountId = this.Account.AccountId,
                Expense = this.Expense,
            };

            entity.CityExpenses = new List<data.Entities.CityExpense>();
            foreach(var cityExpense in this.CityExpenses)
            {
                entity.CityExpenses.Add(cityExpense.MapToEntity());
            }

            return entity;
        }
    }
}
