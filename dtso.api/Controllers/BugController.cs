using dtso.core.Enums;
using dtso.core.Utilties;
using dtso.data.Entities;
using dtso.data.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dtso.api.Controllers
{
    [Route("api/bug")]
    [Authorize]
    public class BugController : Controller
    {
        private IBugRepository _bugRepository;

        public BugController(IBugRepository bugRepository)
        {
            _bugRepository = bugRepository;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetBugs()
        {
            var bugs = _bugRepository.GetAll();
            
            return Ok(bugs);
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddBug([FromBody] Bug form)
        {
            Error error = new Error();
            form.DateCreated = DateTime.Now;
            if (string.IsNullOrEmpty(form.Severity))
            {
                error.ErrorCode = ErrorCode.INVALID;
                error.Message = "Select the severity of the bug.";
            }
            else if (string.IsNullOrEmpty(form.Description))
            {
                error.ErrorCode = ErrorCode.INVALID;
                error.Message = "Please provide the description of the bug.";
            }

            if (error.ErrorCode != ErrorCode.OKAY)
                return BadRequest(error.Message);

            _bugRepository.Add(form);

            return GetBugs();
        }
    }
}
