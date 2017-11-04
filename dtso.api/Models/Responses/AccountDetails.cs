using dtso.api.Utilities;
using dtso.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dtso.api.Models.Responses
{
    public class AccountDetails
    {
        public int? SubNo { get; set; }
        public int? ShredNo { get; set; }
        public string Description { get; set; }
        public decimal AnnualBudget { get; set; }
        public decimal Transfers { get; set; }
        public int AccountNumber { get; set; }
        public decimal ExpedituresToDate { get; set; }
        public Dictionary<int, MonthlyBreakdown> MonthlyDetails { get; set; }

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


        public static AccountDetails MapFromObject(Account account, ResponseGenerator responseGenerator)
        {
            var details = new AccountDetails()
            {
                AccountNumber = account.AccountNumber,
                SubNo = account.SubNo,
                ShredNo = account.ShredNo,
                Description = account.Description,
                ExpedituresToDate = account.ExpedituresToDate,
                Transfers = account.Transfers,
                AnnualBudget = account.AnnualBudget,
            };

            details.MonthlyDetails = new Dictionary<int, MonthlyBreakdown>();
            foreach (var month in account.MonthlyDetails.Keys)
            {
                details.MonthlyDetails[month] = MonthlyBreakdown.MapFromObject(account.MonthlyDetails[month], responseGenerator);
            }

            return details;
        }
    }
}
