using dtso.core.Models;
using dtso.data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace dtso.core.Managers
{
    public class MaterialManager
    {
        private IMaterialRepository _materialRepository;
        public MaterialManager(IMaterialRepository materialRepo)
        {
            _materialRepository = materialRepo;
        }

        public string AddMaterial(MaterialVendor materialVendor)
        {
            if(materialVendor.Material.MaterialId == 0)
            {
                var newMaterial = _materialRepository.Add(materialVendor.Material.MapToEntity());
                materialVendor.Material.MaterialId = newMaterial.MaterialId;
            }

            _materialRepository.Add(materialVendor.MapToEntity());

            return "SUCCESS";
        }

        public List<Material> GetMaterials()
        {
            var materials = new List<Material>();
            
            foreach(var material in _materialRepository.GetAll())
            {
                materials.Add(Material.MapFromEntity(material));
            }

            return materials;
        }

        public Material GetMaterial(int materialId)
        {
            var material = Material.MapFromEntity(_materialRepository.GetMaterial(materialId), true);

            return material;
        }

        public List<MaterialVendor> GetMaterialsForVendor(int vendorId)
        {
            var materials = new List<MaterialVendor>();

            foreach (var material in _materialRepository.GetMaterialForVendor(vendorId))
            {
                materials.Add(MaterialVendor.MapFromEntity(material));
            }

            return materials;
        }
    }
}
