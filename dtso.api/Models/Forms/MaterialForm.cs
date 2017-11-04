using dtso.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dtso.api.Models.Forms
{
    public class MaterialForm
    {
        public int MaterialId { get; set; }
        public int MaterialVendorId { get; set; }
        public int VendorId { get; set; }
        public decimal Cost { get; set; }
        public string Unit { get; set; }
        public string Name { get; set; }

        public MaterialVendor MapToCore()
        {
            var materialVendor = new MaterialVendor()
            {
                MaterialVendorId = this.MaterialVendorId,
                VendorId = this.VendorId,
                Cost = this.Cost
            };

            materialVendor.Material = new Material()
            {
                MaterialId = this.MaterialId,
                Name = this.Name,
                Unit = this.Unit
            };

            return materialVendor;
        }

    }
}
