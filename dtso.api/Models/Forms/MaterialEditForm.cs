using dtso.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dtso.api.Models.Forms
{
    public class MaterialEditForm
    {
        public int MaterialId { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }

        public Material MapToCore()
        {
            return new Material()
            {
                MaterialId = this.MaterialId,
                Name = this.Name,
                Unit = this.Unit
            };
        }
    }
}
