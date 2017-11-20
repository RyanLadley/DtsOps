using dtso.data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace dtso.data.Repositories.Interfaces
{
    public interface IVendorRepository
    {
        int Add(Vendor vendor);
        Vendor Get(int vendorId);
        List<Vendor> GetAll(bool withMaterials, bool onlyActive);
        int Update(Vendor vendor);
    }
}
