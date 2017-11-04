using System;
using System.Collections.Generic;
using System.Text;

namespace dtso.data.Entities
{
    public class Vendor
    {
        public int VendorId { get; set; }
        public string Name { get; set; }
        public string ContractNumber { get; set; }
        public DateTime? ContractStart { get; set; }
        public DateTime? ContractEnd { get; set; }
        public string PointOfContact { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public bool Active { get; set; }

        public List<MaterialVendor> MaterialVendors { get; set; }
        public List<Ticket> Tickets { get; set; }
    }
}
