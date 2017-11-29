using dtso.api.Models.Forms;
using dtso.core.Enums;
using dtso.core.Managers;
using dtso.core.Utilties;
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
            var error = new Error();


            var transferId = _transferManager.AddTransfer(form.MapToCore(), ref error);

            if (error.ErrorCode != ErrorCode.OKAY)
                return BadRequest(error.Message);

            return Ok(transferId);
        }
    }
}
