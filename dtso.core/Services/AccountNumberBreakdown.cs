using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dtso.core.Services
{
    public class AccountNumberBreakdown
    {
        public int? AccountNumber { get; set; }
        public int? SubNo { get; set; }
        public int? ShredNo { get; set; }

        public AccountNumberBreakdown(string accountNumber)
        {
            Parse(accountNumber);
        }

        public AccountNumberBreakdown(Account account)
        {
            AccountNumber = account.AccountNumber;
            SubNo = account.SubNo;
            ShredNo = account.ShredNo;
        }

        public string stringifyAccountNumber()
        {
            if (!AccountNumber.HasValue)
                return "";

            StringBuilder accountNumberBuilder = new StringBuilder(AccountNumber.Value.ToString());

            if (SubNo.HasValue)
            {
                accountNumberBuilder.Append($"-{SubNo.Value.ToString()}");
                if (ShredNo.HasValue)
                {
                    accountNumberBuilder.Append($"-{ShredNo.Value.ToString()}");
                }
            }

            return accountNumberBuilder.ToString();
        }

        public void Parse(string parsableAccountNumber)
        {
            var parts = parsableAccountNumber.Split('-');

            var HasAccountNumber = int.TryParse(parts.ElementAtOrDefault(0), out int accountNumber);
            var HasSubNo = int.TryParse(parts.ElementAtOrDefault(1), out int subNo);
            var HasShredNo = int.TryParse(parts.ElementAtOrDefault(2), out int shredNo);

            AccountNumber = HasAccountNumber ? accountNumber : (int?)null;
            SubNo = HasSubNo ? subNo : (int?)null;
            ShredNo = HasShredNo ? shredNo : (int?)null;

        }

        public bool IsValid()
        {
            return AccountNumber.HasValue
                && (
                        (!SubNo.HasValue && !ShredNo.HasValue) //Has Account Number, but not Sub Or Shred
                     || (SubNo.HasValue && !ShredNo.HasValue) //Has Account and Sub Number, not Shred
                     || (SubNo.HasValue && ShredNo.HasValue) //Has Account Sub and Shred Numbers
                   )
                && !(!SubNo.HasValue && SubNo.HasValue); //Cannot have shred number, but no sub
        }
    }
}
