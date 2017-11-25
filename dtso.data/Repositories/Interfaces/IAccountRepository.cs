using dtso.data.Entities;
using dtso.data.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace dtso.data.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        int Add(Account account);
        vAccount Get(int accountNumber, int? subNumber, int? shredNumber);
        Account GetEntity(int accountId);
        List<vAccount> GetRootAccounts();
        List<vAccount> GetChildAccounts(vAccount account);
        void Remove(int id);
        int Update(Account account);

        List<CityAccount> GetCityAccounts(); 
    }
}
