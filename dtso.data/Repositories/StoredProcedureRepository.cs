using dtso.data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using dtso.data.StoredProcedures;
using dtso.data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace dtso.data.Repositories
{
    public class StoredProcedureRepository : IStoredProcedureRepository
    {
        private MainContext _context;
        
        public StoredProcedureRepository(MainContext context)
        {
            _context = context;

        }

        public List<SearchResult> Search(string searchString)
        {
            var sql = "EXEC Search @searchString={0}";
            return _context.SearchResults.FromSql(sql, searchString).ToList();
        }
       
    }
}
