using dtso.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dtso.api.Models.Responses
{
    public class TransferBasics
    {
        public int TransferId { get; set; }
        public string Description { get; set; }
        
        public AccountListing FromAccount { get; set; }
        public AccountListing ToAccount { get; set; }

        public decimal Amount { get; set; }
        public DateTime DateCreated { get; set; }

        public static TransferBasics MapFromObject(Transfer obj)
        {
            return new TransferBasics()
            {
                TransferId = obj.TransferId,
                Description = obj.Description,
                FromAccount = AccountListing.MapFromObject(obj.FromAccount),
                ToAccount = AccountListing.MapFromObject(obj.ToAccount),
                Amount = obj.Amount,
                DateCreated = obj.DateCreated
            };
        }
    }
}
