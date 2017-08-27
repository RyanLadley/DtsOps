using System;
using System.Collections.Generic;
using System.Text;

namespace dtso.core.Services
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
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }

        public static Vendor MapFromEntity(data.Entities.Vendor entity)
        {
            return new Vendor()
            {
                VendorId = entity.VendorId,
                Name = entity.Name,
                ContractNumber = entity.ContractNumber,
                ContractStart = entity.ContractStart,
                ContractEnd = entity.ContractEnd,
                PointOfContact = entity.PointOfContact,
                PhoneNumber = entity.PhoneNumber,
                Address = entity.Address,
                City = entity.City,
                State = entity.State,
                ZipCode = entity.ZipCode,
                Email = entity.Email,
                Website = entity.Website
            };
        }
    }
}
