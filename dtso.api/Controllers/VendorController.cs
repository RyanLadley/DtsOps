using dtso.api.Models.Forms;
using dtso.api.Models.Responses;
using dtso.api.Utilities;
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
    [Route("api/vendor")]
    [Authorize]
    public class VendorController : Controller
    {
        ResponseGenerator _responseGenerator;
        VendorManager _vendorManager;
        MaterialManager _materialManager;

        public VendorController( ResponseGenerator responseGenerator, VendorManager vendorManager, MaterialManager materialManager)
        {
            _responseGenerator = responseGenerator;
            _vendorManager = vendorManager;
            _materialManager = materialManager;
        }


        [HttpGet]
        public IActionResult GetVendorListing([FromQuery] bool withMaterials)
        {
            var vendors = _vendorManager.GetVendors(withMaterials);

            var response = new List<VendorListing>();
            foreach(var vendor in vendors)
            {
                response.Add(VendorListing.MapFromObject(vendor));
            }
            return Ok(response);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateVendor([FromBody] VendorForm form)
        {
            var error = new Error();
            var materials = new List<MaterialVendor>();
            foreach (var material in form.NewMaterial)
            {
                material.VendorId = form.VendorId;
                materials.Add(material.MapToCore());
            }
            foreach (var material in form.KnownMaterial)
            {
                material.VendorId = form.VendorId;
                materials.Add(material.MapToCore());
            }

            //Validate materials
            foreach (var material in materials)
            {
                _materialManager.ValidateMaterial(material, ref error);
                if (error.ErrorCode != ErrorCode.OKAY)
                    return BadRequest(error.Message);
            }
            
            var vendorId = _vendorManager.CreateVendor(form.MapToCore(), ref error);

            if (error.ErrorCode != ErrorCode.OKAY)
                return BadRequest(error.Message);
            
            return Ok(new { VendorId = vendorId });
        }

        [HttpPost("edit")]
        [Authorize(Roles = "Admin")]
        public IActionResult EditVendor([FromBody] VendorForm form)
        {
            var error = new Error();
            var vendor = _vendorManager.UpdateVendor(form.MapToCore(), ref error);

            if (error.ErrorCode != ErrorCode.OKAY)
                return BadRequest(error.Message);

            return Ok(VendorDetails.MapFromObject(vendor, _responseGenerator));
        }

        [HttpPost("materials")]
        [Authorize(Roles = "Admin")]
        public IActionResult EditMaterials([FromBody] VendorForm form)
        {
            var error = new Error();

            var materials = new List<MaterialVendor>();
            foreach (var material in form.NewMaterial)
            {
                material.VendorId = form.VendorId;
                materials.Add(material.MapToCore());
            }
            foreach (var material in form.KnownMaterial)
            {
                material.VendorId = form.VendorId;
                materials.Add(material.MapToCore());
            }

            //Validate materials
            foreach(var material in materials)
            {
                _materialManager.ValidateMaterial(material, ref error);
                if (error.ErrorCode != ErrorCode.OKAY)
                    return BadRequest(error.Message);
            }
            
            _materialManager.AddMaterials(materials, ref error);


            return GetVendorDetails(form.VendorId);
        }

        [HttpGet("overview")]
        public IActionResult GetVendorOverview()
        {
            var vendors = _vendorManager.GetVendors(false, false);

            var response = new List<VendorOverview>();
            foreach (var vendor in vendors)
            {
                response.Add(VendorOverview.MapFromObject(vendor));
            }
            return Ok(response);
        }

        [HttpGet("{vendorId}")]
        public IActionResult GetVendorDetails(int vendorId)
        {
            var vendor = _vendorManager.GetVendorDetails(vendorId);
       

            if (vendor == null)
                return NoContent();

            var response = VendorDetails.MapFromObject(vendor, _responseGenerator);

            return Ok(response);
        }
    }
}
