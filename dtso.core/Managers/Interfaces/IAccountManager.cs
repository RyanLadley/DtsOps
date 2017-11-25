using dtso.core.Models;
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
        List<Account> PopulateHierarchyExpeditures(List<Account> accounts);

        List<CityAccount> GetCityAccounts();
        List<Account> PopulateHierarchyTransfers(List<Account> accounts);
        void UpdateAccount(Account account);
        void AddAccount(Account account, int parentId);
    }
}
