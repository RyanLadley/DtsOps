using dtso.api.Models.Forms;
using dtso.api.Models.Responses;
using dtso.core.Enums;
using dtso.core.Managers;
using dtso.core.Models;
using dtso.core.Utilties;
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
            var error = new Error();

            if (form.MaterialVendors.Count <= 0)
                return BadRequest("No material vendors were provided");

            var materials = new List<MaterialVendor>();
            foreach (var materialVendor in form.MaterialVendors)
            {
                materials.Add(materialVendor.MapToCore());
            }
            foreach (var materialVendor in materials)
            {
                _materialManager.ValidateMaterial(materialVendor, ref error);

                if (error.ErrorCode != ErrorCode.OKAY)
                    return BadRequest(error.Message);
            }
            foreach (var materialVendor in materials)
            {
                _materialManager.UpdateMaterialVendor(materialVendor);
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
            var error = new Error();
            var material = _materialManager.UpdateMaterial(form.MapToCore(), ref error);

            if (error.ErrorCode != ErrorCode.OKAY)
                return BadRequest(error.Message);

            var response = MaterialDetails.MapFromObject(material);

            return Ok(response);
        }
    }
}
