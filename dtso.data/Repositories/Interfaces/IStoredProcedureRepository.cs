using dtso.data.StoredProcedures;
using System;
using System.Collections.Generic;
using System.Text;

namespace dtso.data.Repositories.Interfaces
{
    public interface IStoredProcedureRepository
    {
        List<SearchResult> Search(string searchString);
    }
}
