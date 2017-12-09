using dtso.data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace dtso.data.Repositories.Interfaces
{
    public interface IBugRepository
    {
        int Add(Bug bug);
        List<Bug> GetAll();

    }
}
