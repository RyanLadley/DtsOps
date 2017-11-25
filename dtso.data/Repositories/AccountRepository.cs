using dtso.data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using dtso.data.Entities;
using dtso.data.Views;
using dtso.data.Context;
using System.Linq;

namespace dtso.data.Repositories
{
    public class AccountRepository : IAccountRepository
    {

        private MainContext _context;

        public AccountRepository(MainContext context)
        {
            _context = context;
        }

        public int Add(Account account)
        {
            _context.Add(account);
            _context.SaveChanges();

            return account.AccountId;
        }

        public vAccount Get(int accountNumber, int? subNumber, int? shredNumber)
        {
            return _context.vAccounts
                .Where(account => account.AccountNumber == accountNumber
                               && account.SubNo == subNumber
                               && account.ShredNo == shredNumber).FirstOrDefault();
        }

        /// <summary>
        /// Returns the "Parent" accounts (No Sub Or Sherd Numbers)
        /// </summary>
        public List<vAccount> GetRootAccounts()
        {
           return _context.vAccounts
                .Where(account => account.SubNo == null
                               && account.ShredNo == null).ToList();
        }

        /// <summary>
        /// Gets the Immediate Children of the provided account
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public List<vAccount> GetChildAccounts(vAccount account)
        {
            if (!account.ShredNo.HasValue && account.SubNo.HasValue)
                return _getShredoutAccounts(account.AccountNumber, account.SubNo.Value);
            
            else if (!account.SubNo.HasValue)
                return _getSubAccounts(account.AccountNumber);

            else
                return new List<vAccount>();
        }

        /// <summary>
        /// Gets Immediate Child Of Root Node (It's sub accounts)
        /// </summary>
        private List<vAccount> _getSubAccounts(int accountNumber)
        {
            return _context.vAccounts
                .Where(account =>  account.AccountNumber == accountNumber
                                && account.SubNo != null
                                && account.ShredNo == null).ToList();
        }

        /// <summary>
        /// Gets the immediate Children of a sub acocunt (It's shred outs)
        /// </summary>
        private List<vAccount> _getShredoutAccounts(int accountNumber, int subNumber)
        {
            return _context.vAccounts
                .Where(account => account.AccountNumber == accountNumber
                                && account.SubNo == subNumber
                                && account.ShredNo != null).ToList();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public int Update(Account account)
        {
            _context.Accounts.Update(account);
            _context.SaveChanges();

            return account.AccountId;
        }

        public List<CityAccount> GetCityAccounts()
        {
            return _context.CityAccounts.ToList();
        }

        public Account GetEntity(int accountId)
        {
            return _context.Accounts.Where(account => account.AccountId == accountId).FirstOrDefault();
        }
    }
}
