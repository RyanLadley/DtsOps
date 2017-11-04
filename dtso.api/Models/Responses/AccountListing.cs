using dtso.core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dtso.api.Models.Responses
{
    public class AccountListing
    {
        public int AccountId { get; set; }
        public int AccountNumber { get; set; }
        public int? SubNo { get; set; }
        public int? ShredNo { get; set; }
        public string Description { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<AccountListing> ChildAccounts { get; set; }

        public static AccountListing MapFromObject(Account account)
        {
            if (account == null)
                return null;

            var listing = new AccountListing()
            {
                AccountId = account.AccountId,
                AccountNumber = account.AccountNumber,
                SubNo = account.SubNo,
                ShredNo = account.ShredNo,
                Description = account.Description,
                ChildAccounts = new List<AccountListing>()
            };

            foreach (var subaccount in account.ChildAccounts)
            {
                listing.ChildAccounts.Add(AccountListing.MapFromObject(subaccount));
            }

            return listing;
        }
    }
}
