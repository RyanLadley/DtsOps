using dtso.core.Managers.Interfaces;
using dtso.core.Services;
using dtso.data.Repositories.Interfaces;
using dtso.data.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace dtso.core.Managers
{
    public class AccountManager : IAccountManager
    {
        private IAccountRepository _accountRepository;

        public AccountManager(IAccountRepository accountRepo)
        {
            _accountRepository = accountRepo;
        }

        /// <summary>
        /// Retreives the complete heiarchyy
        public List<Account> GetOverview()
        {
            var accountViews = _accountRepository.GetRootAccounts();

            List<Account> overview = new List<Account>();
            foreach (var accountView in accountViews)
            {
                var account = _getAccountWithChildren(accountView);
                overview.Add(account);
            }

            return overview;
        }


        /// <summary>
        /// Retrives the children of the provided account. Returns an empty list if it has not children
        /// </summary>
        /// <param name="accountView"></param>
        /// <returns></returns>
        private Account _getAccountWithChildren(vAccount accountView)
        {
            var account = Account.MapFromObject(accountView);
            var childAccounts = _accountRepository.GetChildAccounts(accountView);

            foreach (var childAccount in childAccounts)
            {
                var subAccount = _getAccountWithChildren(childAccount);
                account.Subaccounts.Add(subAccount);
            }

            return account;
        }
    }
}
