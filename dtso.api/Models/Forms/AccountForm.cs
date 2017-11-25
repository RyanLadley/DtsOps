using dtso.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dtso.api.Models.Forms
{
    public class AccountForm
    {
        public int? AccountId { get; set; }
        public int AccountNumber { get; set; }
        public int? SubNo { get; set; }
        public int? ShredNo { get; set; }
        public string Description { get; set; }
        public decimal AnnualBudget { get; set; }

        public int? ParentId { get; set; }

        public Account MapToCore()
        {
            var account = new Account()
            {
                AccountId = (this.AccountId.HasValue) ? this.AccountId.Value : 0,
                AccountNumber = this.AccountNumber,
                SubNo = this.SubNo,
                ShredNo = this.ShredNo,
                Description = this.Description,
                AnnualBudget = this.AnnualBudget
            };
            return account;
        }
    }
}
