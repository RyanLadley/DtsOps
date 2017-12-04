using System;
using System.Collections.Generic;
using System.Text;

namespace dtso.data.Entities
{
    public class CityAccount
    {
        public int CityAccountId { get; set; }
        public string CityAccountNumber { get; set; }
        public string Name { get; set; }

        public List<CityExpense> CityExpenses { get; set; }
    }
}
