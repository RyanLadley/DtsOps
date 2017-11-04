using dtso.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dtso.api.Models.Forms
{
    public class VendorForm
    {
        public string VendorName { get; set; }
        public string ContractNumber { get; set; }
        public DateTime? ContractStart { get; set; }
        public DateTime? ContractEnd { get; set; }
        public string PointOfContact { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string PhoneNumber { get; set; }

        public List<MaterialForm> NewMaterial { get; set; }
        public List<MaterialForm> KnownMaterial { get; set; }

        public Vendor MapToCore()
        {
            return new Vendor()
            {
                Name = this.VendorName,
                ContractNumber = this.ContractNumber,
                ContractStart = this.ContractStart,
                ContractEnd = this.ContractEnd,
                PointOfContact = this.PointOfContact,
                PhoneNumber = this.PhoneNumber,
                Email = this.Email,
                Website = this.Website
            };
        }
    }
}
