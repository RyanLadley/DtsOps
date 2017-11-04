using dtso.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dtso.api.Models.Responses
{
    public class CityAccountListing
    {
        public int CityAccountId { get; set; }
        public string Name { get; set; }

        public static CityAccountListing MapFromObject(CityAccount cityAccount)
        {
            return new CityAccountListing()
            {
                CityAccountId = cityAccount.CityAccountId,
                Name = cityAccount.Name
            };
        }
    }
}
