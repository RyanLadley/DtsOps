using dtso.api.Models.Forms;
using dtso.core.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dtso.api.Controllers
{
    [Route("api/transfer")]
    [Authorize]
    public class TransferController : Controller
    {
        private TransferManager _transferManager;

        public TransferController(TransferManager tansferMan)
        {
            _transferManager = tansferMan;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateTransfer([FromBody] TransferForm form)
        {
            var transferId = _transferManager.AddTransfer(form.MapToCore());
            return Ok(transferId);
        }
    }
}
