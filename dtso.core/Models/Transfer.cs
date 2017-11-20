using System;
using System.Collections.Generic;
using System.Text;

namespace dtso.core.Models
{
    public class Transfer
    {
        public int TransferId { get; set; }
        public string Description { get; set; }
        
        public Account FromAccount { get; set; }

        public Account ToAccount { get; set; }

        public DateTime DateCreated { get; set; }
        public decimal Amount { get; set; }

        public static Transfer MapFromEntity(data.Entities.Transfer entity)
        {
            var transfer = new Transfer()
            {
                TransferId = entity.TransferId,
                Description = entity.Description,
                ToAccount = Account.MapFromObject(entity.TovAccount),
                FromAccount = Account.MapFromObject(entity.FromvAccount),
                DateCreated = entity.DateCreated,
                Amount = entity.Amount
            };

            return transfer;
        }

        public data.Entities.Transfer MapToEntity()
        {
            var entity = new data.Entities.Transfer()
            {
                TransferId = this.TransferId,
                Description = this.Description,
                ToAccountId = this.ToAccount.AccountId,
                FromAccountId = this.FromAccount.AccountId,
                DateCreated = this.DateCreated,
                Amount = this.Amount
            };

            return entity;
        }


    }
}
