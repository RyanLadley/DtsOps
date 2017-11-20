using System;
using System.Collections.Generic;
using System.Text;

namespace dtso.data.StoredProcedures
{
    public class SearchResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SubName { get; set; }
        public decimal? Expense { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public int? Relavance { get; set; }
    }
}
