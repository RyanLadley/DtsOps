using dtso.core.Managers.Interfaces;
using dtso.core;
using Microsoft.AspNetCore.Mvc;
using dtso.api.ResponseGenerators;
using dtso.api.Models.Responses;
using System.Collections.Generic;

namespace dtso.api.Controllers
{
    [Route("api/account")]
    public class AccountController : Controller
    {
        private IAccountManager _accountManager;
        AccountResponseGenerator _responseGenerator;
        public AccountController(IAccountManager accountMan)
        {
            _accountManager = accountMan;
            _responseGenerator = new AccountResponseGenerator();
        }

        [HttpGet]
        public IActionResult Overview()
        {
            var accounts = _accountManager.GetOverview();

            var response = new List<AccountOverview>();
            foreach(var account in accounts)
            {
                response.Add(_responseGenerator.GenerateOverview(account));
            }

            return Ok(response);
        }
    }
}
