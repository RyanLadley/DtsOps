using dtso.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dtso.api.Models.Responses
{
    public class CityExpenseBasic
    {
        public int CityExpenseId { get; set; }
        public int CityAccountId { get; set; }
        public string Name { get; set; }
        public decimal Expense { get; set; }

        public static CityExpenseBasic MapFromObject(CityExpense obj)
        {
            if (obj == null)
                return null;

            return new CityExpenseBasic()
            {
                CityExpenseId = obj.CityExpenseId,
                CityAccountId = obj.CityAccount.CityAccountId,
                Name = obj.CityAccount.Name,
                Expense = obj.Expense
            };
        }
    }
}
