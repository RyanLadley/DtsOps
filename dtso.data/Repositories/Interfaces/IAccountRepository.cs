using dtso.data.Entities;
using dtso.data.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace dtso.data.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        vAccount Add(Account account);
        vAccount Get(int accountNumber, int? subNumber, int? shredNumber);
        List<vAccount> GetRootAccounts();
        List<vAccount> GetChildAccounts(vAccount account);
        void Remove(int id);
        vAccount Update(int id);

        List<CityAccount> GetCityAccounts(); 
    }
}
