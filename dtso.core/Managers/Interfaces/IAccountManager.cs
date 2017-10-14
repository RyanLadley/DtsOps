using dtso.core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace dtso.core.Managers.Interfaces
{
    public interface IAccountManager
    {
        List<Account> GetHierarchy();
        decimal GetExpedituresToDate(AccountNumberTemplate accountNumber);
        Account GetAccountDetails(AccountNumberTemplate accountNumber);
        List<Account> PopulateExpeditures(List<Account> accounts);
    }
}
