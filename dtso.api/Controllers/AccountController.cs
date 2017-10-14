using dtso.core.Managers.Interfaces;
using dtso.core;
using Microsoft.AspNetCore.Mvc;
using dtso.api.Models.Responses;
using System.Collections.Generic;
using dtso.core.Services;
using dtso.api.Utilities;

namespace dtso.api.Controllers
{
    [Route("api/account")]
    public class AccountController : Controller
    {
        private IAccountManager _accountManager;
        ResponseGenerator _responseGenerator;

        public AccountController(IAccountManager accountMan, ResponseGenerator responseGenerator)
        {
            _accountManager = accountMan;
            _responseGenerator = responseGenerator;
        }

        [HttpGet]
        public IActionResult Overview()
        {
            var accounts = _accountManager.GetHierarchy();
            accounts = _accountManager.PopulateExpeditures(accounts);

            var response = new List<AccountOverview>();
            foreach (var account in accounts)
            {
                response.Add(AccountOverview.MapFromObject(account));
            }

            return Ok(response);
        }

        [HttpGet("{accountNumber}")]
        public IActionResult GetAccount(string accountNumber)
        {
            var parsedAccountNumber = new AccountNumberTemplate(accountNumber);

            if (!parsedAccountNumber.IsValid())
                return BadRequest("Account Number is invalid");

            var account = _accountManager.GetAccountDetails(parsedAccountNumber);

            var response = AccountDetails.MapFromObject(account, _responseGenerator);

            return Ok(response);
        }
    }
}
