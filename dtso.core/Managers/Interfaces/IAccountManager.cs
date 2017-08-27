using dtso.core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace dtso.core.Managers.Interfaces
{
    public interface IAccountManager
    {
        List<Account> GetOverview();
    }
}
