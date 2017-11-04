using System;
using System.Collections.Generic;
using System.Text;

namespace dtso.core.Models
{
    public class MaterialVendor
    {
        public int MaterialVendorId { get; set; }
        public decimal Cost { get; set; }

        public Material Material { get; set; }

        public int VendorId { get; set; }

        public data.Entities.MaterialVendor MapToEntity()
        {
            return new data.Entities.MaterialVendor()
            {
                MaterialVendorId = this.MaterialVendorId,
                Cost = this.Cost,
                VendorId = this.VendorId,
                MaterialId = this.Material.MaterialId,
            };
        }

        public static MaterialVendor MapFromEntity(data.Entities.MaterialVendor entity)
        {
            return new MaterialVendor()
            {
                MaterialVendorId = entity.MaterialVendorId,
                Cost = entity.Cost,
                VendorId = entity.VendorId,
                Material = Material.MapFromEntity(entity.Material)
            };
        }
    }
}
