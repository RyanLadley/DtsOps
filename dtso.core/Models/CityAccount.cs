using System;
using System.Collections.Generic;
using System.Text;

namespace dtso.core.Models
{
    public class CityAccount
    {
        public int CityAccountId { get; set; }
        public string Name { get; set; }
        public string CityAccountNumber { get; set; }
        public static CityAccount MapFromEntity(data.Entities.CityAccount entity)
        {
            if (entity == null)
                return null;

            return new CityAccount()
            {
                CityAccountId = entity.CityAccountId,
                Name = entity.Name,
                CityAccountNumber = entity.CityAccountNumber
            };
        }

    }
}
