using dtso.data.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace dtso.core.Services
{
    public class Account
    {
        public int AccountId { get; set; }
        public int? SubNo { get; set; }
        public int? ShredNo { get; set; }
        public string Description { get; set; }
        public decimal AnnualBudget { get; set; }

        public int AccountNumber { get; set; }
        public int FundNumber { get; set; }
        public int DeptartmentNumber { get; set; }
        public string ProjectNumber { get; set; }
        public string ProjectDescription { get; set; }
        public string AccountPrefix { get; set; }

        public List<Account> Subaccounts { get; set; }
        
        public static Account MapFromObject(vAccount view)
        {
            var account = new Account()
            {
                SubNo = (view.SubNo >= 0) ? view.SubNo : (int?)null,
                ShredNo = (view.ShredNo >= 0) ? view.ShredNo : (int?)null,
                Description = view.Description,
                AnnualBudget = view.AnnualBudget,
                AccountNumber = view.AccountNumber,
                FundNumber = view.FundNumber,
                ProjectNumber = view.ProjectNumber,
                AccountPrefix = view.AccountPrefix,
                Subaccounts = new List<Account>()
            };

            return account;
        }
    }
}
