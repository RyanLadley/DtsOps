using dtso.api.Utilities;
using dtso.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dtso.api.Models.Responses
{
    public class VendorDetails
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
        public List<InvoiceBasic> Invoices { get; set; }
        public List<TicketBasic> Tickets { get; set; }
        public List<MaterialVendorListing> Materials { get; set; }

        public static VendorDetails MapFromObject(Vendor obj, ResponseGenerator responseGenerator)
        {
            var materials = new List<MaterialVendorListing>();
            foreach (var material in obj.Materials)
            {
                materials.Add(MaterialVendorListing.MapFromObject(material));
            }
            return new VendorDetails()
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
                Active = obj.Active,
                Invoices = responseGenerator.GenerateBasicInvoicesList(obj.Invoices),
                Tickets = responseGenerator.GenerateBasicTicketList(obj.Tickets),
                Materials = materials
            };
        }
    }
}
