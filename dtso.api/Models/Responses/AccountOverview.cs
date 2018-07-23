using dtso.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dtso.api.Models.Responses
{
    public class AccountOverview
    {
        public int AccountId { get; set; }
        public int AccountNumber { get; set; }
        public int? SubNo { get; set; }
        public int? ShredNo { get; set; }
        public string Description { get; set; }
        public decimal ExpedituresToDate { get; set; }
        public decimal Transfers { get; set; }
        public decimal AnnualBudget { get; set; }

        public decimal TotalBudget
        {
            get
            {
                return AnnualBudget + Transfers; //Transfers Have correct signage to reflect to or from
            }
        }

        public decimal RemainingBalance
        {
            get
            {
                return TotalBudget - ExpedituresToDate;
            }
        }

        public List<AccountOverview> ChildAccounts { get; set; }

        public static AccountOverview MapFromObject(Account account)
        {
            var overview = new AccountOverview()
            {
                AccountId = account.AccountId,
                AccountNumber = account.AccountNumber,
                SubNo = account.SubNo,
                ShredNo = account.ShredNo,
                Description = account.Description,
                ExpedituresToDate = account.ExpedituresToDate,
                Transfers = account.Transfers,
                AnnualBudget = account.AnnualBudget,
                ChildAccounts = new List<AccountOverview>()
            };

            foreach (var subaccount in account.ChildAccounts)
            {
                overview.ChildAccounts.Add(AccountOverview.MapFromObject(subaccount));
            }

            return overview;
        }
    }
}
