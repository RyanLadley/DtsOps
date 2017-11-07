using dtso.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dtso.api.Models.Responses
{
    public class MaterialVendorDetails
    {
        public int MaterialVendorId { get; set; }

        public decimal Cost { get; set; }
        public VendorListing Vendor { get; set; }


        public static MaterialVendorDetails MapFromObject(MaterialVendor obj)
        {
            return new MaterialVendorDetails()
            {
                MaterialVendorId = obj.MaterialVendorId,
                Cost = obj.Cost,
                Vendor = VendorListing.MapFromObject(obj.Vendor)
            };
        }
    }
}
