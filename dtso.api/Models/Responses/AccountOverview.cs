using dtso.core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dtso.api.Models.Responses
{
    public class AccountOverview
    {
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
                return AnnualBudget - Transfers;
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
