using dtso.data.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace dtso.core.Models
{
    public class Account
    {
        public int AccountId { get; set; }
        public int? SubNo { get; set; }
        public int? ShredNo { get; set; }
        public string Description { get; set; }
        public decimal AnnualBudget { get; set; }
        public decimal Transfers { get; set; }
        public int AccountNumber { get; set; }
        public int FundNumber { get; set; }
        public int DeptartmentNumber { get; set; }
        public string ProjectNumber { get; set; }
        public string ProjectDescription { get; set; }
        public string AccountPrefix { get; set; }
        public decimal ExpedituresToDate { get; set; }
        public List<Account> ChildAccounts { get; set; }
        public Dictionary<int, MonthlyBreakdown> MonthlyDetails { get; set; }

        public static Account MapFromObject(vAccount view)
        {
            if (view == null)
                return null;

            var account = new Account()
            {
                AccountId = view.AccountId,
                SubNo = (view.SubNo >= 0) ? view.SubNo : (int?)null,
                ShredNo = (view.ShredNo >= 0) ? view.ShredNo : (int?)null,
                Description = view.Description,
                AnnualBudget = view.AnnualBudget,
                Transfers = 0,
                AccountNumber = view.AccountNumber,
                FundNumber = view.FundNumber,
                ProjectNumber = view.ProjectNumber,
                AccountPrefix = view.AccountPrefix,
                ChildAccounts = new List<Account>()
            };

            return account;
        }
    }
}
