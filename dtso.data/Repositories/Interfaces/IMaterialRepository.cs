using dtso.data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace dtso.data.Repositories.Interfaces
{
    public interface IMaterialRepository
    {
        Material Add(Material material);
        MaterialVendor Add(MaterialVendor materialVendor);
        List<Material> GetAll();
        List<MaterialVendor> GetMaterialForVendor(int vendorId);
        Material GetMaterial(int materialId);
    }
}
