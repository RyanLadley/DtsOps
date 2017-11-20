using dtso.data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using dtso.data.Entities;
using dtso.data.Context;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace dtso.data.Repositories
{
    public class VendorRepository : IVendorRepository
    {
        MainContext _context;

        public VendorRepository(MainContext context)
        {
            _context = context;
        }

        public int Add(Vendor vendor)
        {
            try
            {
                _context.Add(vendor);
                _context.SaveChanges();
                return vendor.VendorId;
            }
            catch(Exception ex)
            {
                return 0;
            }
        }

        public Vendor Get(int vendorId)
        {
            return _context.Vendors
                .Where(vendor => vendor.VendorId == vendorId)
                .FirstOrDefault();
        }

        public List<Vendor> GetAll(bool withMaterials, bool onlyActive)
        {
            var vendors = _context.Vendors
                .Where(vendor => ((onlyActive && vendor.Active)
                                || (!onlyActive))
                             //Get Only Vendors With Material Check
                              && (withMaterials && _context.MaterialVendors.Any(material => material.VendorId == vendor.VendorId)
                                || (!withMaterials))).ToList();
            

            return vendors;
        }

        public int Update(Vendor vendor)
        {
            _context.Vendors.Update(vendor);
            _context.SaveChanges();

            return vendor.VendorId;
        }
    }
}
