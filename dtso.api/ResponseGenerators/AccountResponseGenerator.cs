using dtso.api.Models.Responses;
using dtso.core.Services;
using System.Collections.Generic;

namespace dtso.api.ResponseGenerators
{
    public class AccountResponseGenerator
    {
        public AccountOverview GenerateOverview(Account account)
        {
            var overview = new AccountOverview()
            {
                AccountNumber = account.AccountNumber,
                SubNo = account.SubNo,
                ShredNo = account.ShredNo,
                Description = account.Description,
                AnnualBudget = account.AnnualBudget,
                Subaccounts = new List<AccountOverview>()
            };

            foreach(var subaccount in account.Subaccounts)
            {
                overview.Subaccounts.Add(GenerateOverview(subaccount));
            }

            return overview;
        }
    }
}
