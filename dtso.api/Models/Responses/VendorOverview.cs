using dtso.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dtso.api.Models.Responses
{
    public class VendorOverview
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
        public string Status { get; set; }
        public bool Active { get; set; }

        public static VendorOverview MapFromObject(Vendor obj)
        {
            return new VendorOverview()
            {
                VendorId = obj.VendorId,
                Name = obj.Name,
                ContractStart = obj.ContractStart,
                ContractEnd = obj.ContractEnd,
                ContractNumber = obj.ContractNumber,
                PointOfContact = obj.PointOfContact,
                PhoneNumber = obj.PhoneNumber,
                Email = obj.Email,
                Website = obj.Website,
                Status = (obj.Active) ? "Active" : "Inactive",
                Active = obj.Active
            };
        }
    }
}
