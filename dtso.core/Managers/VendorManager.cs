using dtso.core.Enums;
using dtso.core.Managers.Interfaces;
using dtso.core.Models;
using dtso.core.Utilties;
using dtso.data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace dtso.core.Managers
{
    public class VendorManager
    {
        private IVendorRepository _vendorRepository;
        private IInvoiceManager _invoiceManager;
        private TicketManager _ticketManager;
        private MaterialManager _materialManager;

        public VendorManager(IVendorRepository vendorRepo, IInvoiceManager invoiceMan, TicketManager ticketMan, MaterialManager materialMan)
        {
            _vendorRepository = vendorRepo;
            _invoiceManager = invoiceMan;
            _ticketManager = ticketMan;
            _materialManager = materialMan;
        }

        public int CreateVendor(Vendor vendor, ref Error error)
        {
            _validateVendorForm(vendor, ref error);

            if (error.ErrorCode != ErrorCode.OKAY)
                return -1;

            vendor.Active = true;
            return _vendorRepository.Add(vendor.MapToEntity());
            
        }

        public List<Vendor> GetVendors(bool withMaterials, bool onlyActive = true)
        {
            var vendors = new List<Vendor>();
            foreach(var vendor in _vendorRepository.GetAll(withMaterials, onlyActive))
            {
                vendors.Add(Vendor.MapFromEntity(vendor));
            }
            
            return vendors;
        }

        public Vendor GetVendorDetails(int vendorId)
        {
            var vendor = Vendor.MapFromEntity(_vendorRepository.Get(vendorId));
            vendor.Invoices = _invoiceManager.GetInvoicesForVendor(vendorId);
            vendor.Tickets = _ticketManager.GetTicketsForVendor(vendorId);
            vendor.Materials = _materialManager.GetMaterialsForVendor(vendorId);

            return vendor;
        }

        public Vendor UpdateVendor(Vendor vendor, ref Error error)
        {
            _validateVendorForm(vendor, ref error);

            if (error.ErrorCode != ErrorCode.OKAY)
                return null;

            var vendorId = _vendorRepository.Update(vendor.MapToEntity());

            return GetVendorDetails(vendorId);
        }

        private void _validateVendorForm(Vendor vendor, ref Error error)
        {
            if (string.IsNullOrEmpty(vendor.Name))
            {
                error.ErrorCode = ErrorCode.INVALID;
                error.Message = "The Vendor's Name Must Be Provided";
            }
            else if (string.IsNullOrEmpty(vendor.ContractNumber))
            {
                error.ErrorCode = ErrorCode.INVALID;
                error.Message = "A Contract Number Mut Be Provided";
            }
        }
    }
}
