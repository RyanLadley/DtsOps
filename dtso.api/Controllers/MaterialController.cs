using dtso.api.Models.Forms;
using dtso.api.Models.Responses;
using dtso.core.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dtso.api.Controllers
{
    [Route("api/material")]
    [Authorize]
    public class MaterialController : Controller
    {
        MaterialManager _materialManager;

        public MaterialController(MaterialManager materialManager)
        {
            _materialManager = materialManager;
        }

        [HttpGet]
        public IActionResult GetMaterialListing()
        {
            var materials = _materialManager.GetMaterials();

            var response = new List<MaterialListing>();
            foreach (var material in materials)
            {
                response.Add(MaterialListing.MapFromObject(material));
            }
            return Ok(response);
        }

        [HttpGet("{materialId}")]
        public IActionResult GetMaterial(int materialId)
        {
            var material = _materialManager.GetMaterial(materialId);

            var response = MaterialDetails.MapFromObject(material);

            return Ok(response);
        }

        [HttpPost("vendor/edit")]
        [Authorize(Roles = "Admin")]
        public IActionResult EditMaterialVendors([FromBody] MaterialVendorsEditForm form)
        {
            foreach(var materialVendor in form.MaterialVendors)
            {
                _materialManager.UpdateMaterialVendor(materialVendor.MapToCore());
            }

            var material = _materialManager.GetMaterial(form.MaterialVendors[0].MaterialId);
            var response = MaterialDetails.MapFromObject(material);

            return Ok(response);
        }

        [HttpGet("vendor/{vendorId}")]
        public IActionResult GetMaterialForVendor(int vendorId)
        {
            var materials = _materialManager.GetMaterialsForVendor(vendorId);

            var response = new List<MaterialVendorListing>();
            foreach (var material in materials)
            {
                response.Add(MaterialVendorListing.MapFromObject(material));
            }
            return Ok(response);
        }

        [HttpPost("edit")]
        [Authorize(Roles = "Admin")]
        public IActionResult EditMaterial([FromBody] MaterialEditForm form)
        {
            var material = _materialManager.UpdateMaterial(form.MapToCore());

            var response = MaterialDetails.MapFromObject(material);

            return Ok(response);
        }
    }
}
