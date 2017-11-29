using dtso.core.Enums;
using dtso.core.Models;
using dtso.core.Utilties;
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

        public void UpdateMaterialVendor(MaterialVendor materialVendor)
        {
            if(materialVendor.MaterialVendorId > 0)
            {
                _materialRepository.Update(materialVendor.MapToEntity());
            }
            else
            {
                AddMaterial(materialVendor);
            }
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

        public Material UpdateMaterial(Material material, ref Error error)
        {
            _validateMaterial(material, ref error);

            if (error.ErrorCode != ErrorCode.OKAY)
                return null;

            var materialId = _materialRepository.Update(material.MapToEntity());

            return GetMaterial(materialId);
        }

        private void _validateMaterial(Material material, ref Error error)
        {
            if (string.IsNullOrEmpty(material.Name))
            {
                error.ErrorCode = ErrorCode.INVALID;
                error.Message = "A Material Name Must Be Provided";
            }
            else if (string.IsNullOrEmpty(material.Unit))
            {
                error.ErrorCode = ErrorCode.INVALID;
                error.Message = "The Material Unit Must Be Provided";
            }
        }
    }
}
