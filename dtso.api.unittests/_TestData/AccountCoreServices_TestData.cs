
using dtso.core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace dtso.api.unittests._TestData
{
    public class AccountCoreServices_TestData
    {
        public Account SingleAccount()
        {

            var account = new Account()
            {
                AccountId = 1,
                AccountNumber = 1234,
                Description = $"Test Account",
                AnnualBudget = (decimal)100.00,
                FundNumber = 96,
                ProjectNumber = $"Test Project",
                AccountPrefix = $"-Test",
                ChildAccounts = new List<Account>()
            };
        
            return account;
        }

        public Account SingleAccountWithChildren()
        {
            var account = SingleAccount();

            account.ChildAccounts = ChildAccounts(account);
            foreach(var subaccount in account.ChildAccounts)
            {
                subaccount.ChildAccounts = ChildAccounts(subaccount);
            }

            return account;
        }

        public List<Account> ChildAccounts(Account account)
        {
            List<Account> childAccounts = new List<Account>();

            if (!account.ShredNo.HasValue)
            {
                for (var i = 0; i < 2; i++)
                {
                    childAccounts.Add(new Account()
                    {
                        AccountId = i,
                        AccountNumber = account.AccountNumber,
                        SubNo = (account.SubNo.HasValue) ? account.SubNo : i,
                        ShredNo = (account.SubNo.HasValue) ? i : (int?)null,
                        Description = $"Child Account {i}",
                        AnnualBudget = (decimal)i,
                        FundNumber = i,
                        ProjectNumber = $"Project {i}",
                        AccountPrefix = $"-AA{i}",
                        ChildAccounts = new List<Account>()
                    });
                }
            }

            return childAccounts;
        }
    }
}
