using dtso.data.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace dtso.core.unittests._TestData
{
    public class vAccounts_TestData
    {
        public vAccount SingleAccount()
        {
            
            var account = new vAccount()
            {
                AccountNumber = 1234,
                SubNo = 5,
                ShredNo = 6,
                Description = $"Test Account",
                AnnualBudget = (decimal)100.00,
                FundNumber = 96,
                ProjectNumber = $"Test Project",
                AccountPrefix = $"-Test",
                RegionalAccountCodeId = 987
            };

            return account;
        }

        public List<vAccount> RootAccounts()
        {
            List<vAccount> accounts = new List<vAccount>();

            for (var i = 0; i < 2; i++)
            {
                accounts.Add(new vAccount()
                {
                    AccountNumber = i,
                    SubNo = null,
                    ShredNo = null,
                    Description = $"Account {i}",
                    AnnualBudget = (decimal)i,
                    FundNumber = i,
                    ProjectNumber = $"Project {i}",
                    AccountPrefix = $"-AA{i}",
                    RegionalAccountCodeId = i
                });
            }

            return accounts;
        }

        public List<vAccount> ChildAccounts(vAccount account)
        {
            List<vAccount> childAccounts = new List<vAccount>();

            if (!account.ShredNo.HasValue)
            { 
                for (var i = 0; i < 2; i++)
                {
                    childAccounts.Add(new vAccount()
                    {
                        AccountNumber = account.AccountNumber,
                        SubNo = (account.SubNo.HasValue) ? account.SubNo : i,
                        ShredNo = (account.SubNo.HasValue) ? i : -1,
                        Description = $"Child Account {i}",
                        AnnualBudget = (decimal)i,
                        FundNumber = i,
                        ProjectNumber = $"Project {i}",
                        AccountPrefix = $"-AA{i}",
                        RegionalAccountCodeId = i
                    });
                }
            }

            return childAccounts;
        }

    }
}
