using System;
using System.Collections.Generic;
using System.Text;

namespace dtso.core.Services
{
    public class InvoiceAccountTotal
    {
        public Account Account { get; set; }
        public decimal Expense { get; set; }

        public static InvoiceAccountTotal MapFromEntity(data.Entities.InvoiceAccount entity)
        {
            return new InvoiceAccountTotal()
            {
                Account = Account.MapFromObject(entity.vAccount),
                Expense = entity.Expense
            };
        }
    }
}
