using dtso.api.Models.Forms;
using dtso.api.Models.Responses;
using dtso.api.Utilities;
using dtso.core.Managers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dtso.api.Controllers
{
    [Route("api/vendor")]
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
        public IActionResult CreateVendor([FromBody] VendorForm form)
        {
            var vendorId = _vendorManager.CreateVendor(form.MapToCore());
           
            foreach(var material in form.NewMaterial)
            {
                material.VendorId = vendorId;
                _materialManager.AddMaterial(material.MapToCore());
            }
            foreach(var material in form.KnownMaterial)
            {
                material.VendorId = vendorId;
                _materialManager.AddMaterial(material.MapToCore());
            }

            if (vendorId > 0)
                return Ok(new { VendorId = vendorId });
            else
                return BadRequest("There was an error processing your request");
        }

        [HttpGet("overview")]
        public IActionResult GetVendorOverview()
        {
            var vendors = _vendorManager.GetVendors(false);

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
