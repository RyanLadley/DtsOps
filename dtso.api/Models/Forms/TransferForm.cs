using dtso.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dtso.api.Models.Forms
{
    public class TransferForm
    {
        public int FromAccountId { get; set; }
        public int ToAccountId { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }

        public Transfer MapToCore()
        {
            var from = new Account
            {
                AccountId = FromAccountId
            };

            var to = new Account
            {
                AccountId = ToAccountId
            };

            var transfer = new Transfer()
            {
                ToAccount = to,
                FromAccount = from,
                Description = this.Description,
                Amount = this.Amount,
                DateCreated = DateTime.Now
            };

            return transfer;
        }
    }
}
