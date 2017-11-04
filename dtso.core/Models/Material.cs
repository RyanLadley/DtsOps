﻿using System;
using System.Collections.Generic;
using System.Text;

namespace dtso.core.Models
{
    public class Material
    {
        public int MaterialId { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }

        public List<MaterialVendor> MaterialVendors { get; set; }

        public data.Entities.Material MapToEntity()
        {
            return new data.Entities.Material()
            {
                MaterialId = this.MaterialId,
                Name = this.Name,
                Unit = this.Unit
            };
        }

        public static Material MapFromEntity(data.Entities.Material entity)
        {
            return new Material()
            {
                MaterialId = entity.MaterialId,
                Name = entity.Name,
                Unit = entity.Unit
            };
        }
    }
}
