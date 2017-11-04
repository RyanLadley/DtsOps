using System;
using System.Collections.Generic;
using System.Text;

namespace dtso.core.Models
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

        //Not Populated From Entity
        public List<Invoice> Invoices { get; set; }
        public List<Ticket> Tickets { get; set; }
        public List<MaterialVendor> Materials { get; set; }


        public static Vendor MapFromEntity(data.Entities.Vendor entity)
        {
            if (entity == null)
                return null;

            return new Vendor()
            {
                VendorId = entity.VendorId,
                Name = entity.Name,
                ContractNumber = entity.ContractNumber,
                ContractStart = entity.ContractStart,
                ContractEnd = entity.ContractEnd,
                PointOfContact = entity.PointOfContact,
                PhoneNumber = entity.PhoneNumber,
                Email = entity.Email,
                Website = entity.Website,
                Active = entity.Active
            };
        }

        public data.Entities.Vendor MapToEntity()
        {
            return new data.Entities.Vendor()
            {
                VendorId = this.VendorId,
                Name = this.Name,
                ContractNumber = this.ContractNumber,
                ContractStart = this.ContractStart,
                ContractEnd = this.ContractEnd,
                PointOfContact = this.PointOfContact,
                PhoneNumber = this.PhoneNumber,
                Email = this.Email,
                Website = this.Website,
                Active = this.Active
            };
        }
    }
}
