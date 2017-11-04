using dtso.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dtso.api.Models.Responses
{
    public class VendorListing
    {
        public int VendorId { get; set; }
        public string Name { get; set; }

        public static VendorListing MapFromObject(Vendor obj)
        {
            return new VendorListing()
            {
                VendorId = obj.VendorId,
                Name = obj.Name
            };
        }
    }
}
