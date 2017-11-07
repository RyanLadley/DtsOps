using dtso.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dtso.api.Models.Responses
{
    public class MaterialDetails
    {
        public int MaterialId { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }

        public List<MaterialVendorDetails> MaterialVendors { get; set; }

        public static MaterialDetails MapFromObject(Material obj)
        {
            var materialVendors = new List<MaterialVendorDetails>();
            foreach(var materialVendor in obj.MaterialVendors)
            {
                materialVendors.Add(MaterialVendorDetails.MapFromObject(materialVendor));
            }


            return new MaterialDetails()
            {
                MaterialId = obj.MaterialId,
                Name = obj.Name,
                Unit = obj.Unit,
                MaterialVendors = materialVendors
            };
        }
    }

}
