using System;
using System.Collections.Generic;
using System.Text;

namespace dtso.data.Entities
{
    public class Material
    {
        public int MaterialId { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }

        public List<MaterialVendor> MaterialVendors { get; set; }
}
}
