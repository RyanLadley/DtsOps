using dtso.data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using dtso.data.Entities;
using dtso.data.Context;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace dtso.data.Repositories
{
    public class MaterialRepository : IMaterialRepository
    {
        private MainContext _context;

        public MaterialRepository(MainContext context)
        {
            _context = context;
        }
        public Material Add(Material material)
        {
            _context.Add(material);
            _context.SaveChanges();
            return material;
        }

        public MaterialVendor Add(MaterialVendor materialVendor)
        {
            _context.Add(materialVendor);
            _context.SaveChanges();

            return materialVendor;
        }

        public List<Material> GetAll()
        {
            return _context.Materials.ToList();
        }

        public Material GetMaterial(int materialId)
        {
            return _context.Materials
                .Where(material => material.MaterialId == materialId)
                .Include(material => material.MaterialVendors)
                    .ThenInclude(materialVendor => materialVendor.Vendor)
                .FirstOrDefault();
        }

        public List<MaterialVendor> GetMaterialForVendor(int vendorId)
        {
            return _context.MaterialVendors
                    .Where(materialVendor => materialVendor.VendorId == vendorId)
                    
                    .Include(materialVendor => materialVendor.Material)
                    .ToList();
        }
        

        public int Update(Material material)
        {
            _context.Materials.Update(material);
            _context.SaveChanges();

            return material.MaterialId;
        }

        public int Update(MaterialVendor materialVendor)
        {
            _context.MaterialVendors.Update(materialVendor);
            _context.SaveChanges();

            return materialVendor.MaterialVendorId;
        }
    }
}
