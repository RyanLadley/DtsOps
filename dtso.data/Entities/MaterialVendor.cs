using System;
using System.Collections.Generic;
using System.Text;

namespace dtso.data.Entities
{
    public class MaterialVendor
    {
        public int MaterialVendorId { get; set; }
        public decimal Cost { get; set; }

        public int MaterialId { get; set; }
        public Material Material { get; set; }

        public int VendorId { get; set; }
        public Vendor Vendor { get; set; }
        public List<Ticket> Tickets { get; set; }

    }
}
