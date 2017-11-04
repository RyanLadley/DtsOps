using dtso.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dtso.api.Models.Responses
{
    public class MaterialVendorListing
    {
        public int MaterialVendorId { get; set; }
        public int MaterialId { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        
        public decimal Cost { get; set; }
        public int VendorId { get; set; }
        

        public static MaterialVendorListing MapFromObject(MaterialVendor obj)
        {
            return new MaterialVendorListing()
            {
                MaterialId = obj.Material.MaterialId,
                MaterialVendorId =obj.MaterialVendorId,
                Name = obj.Material.Name,
                Unit = obj.Material.Unit,
                Cost = obj.Cost,
                VendorId = obj.VendorId
            };
        }
    }
}
