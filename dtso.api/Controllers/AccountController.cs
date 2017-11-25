using dtso.core.Managers.Interfaces;
using dtso.core;
using Microsoft.AspNetCore.Mvc;
using dtso.api.Models.Responses;
using System.Collections.Generic;
using dtso.core.Models;
using dtso.api.Utilities;
using Microsoft.AspNetCore.Authorization;
using dtso.api.Models.Forms;

namespace dtso.api.Controllers
{
    [Route("api/account")]
    [Authorize]
    public class AccountController : Controller
    {
        private IAccountManager _accountManager;
        ResponseGenerator _responseGenerator;

        public AccountController(IAccountManager accountMan, ResponseGenerator responseGenerator)
        {
            _accountManager = accountMan;
            _responseGenerator = responseGenerator;
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateAccounts([FromBody] List<AccountForm> forms)
        {
            foreach(var form in forms)
            {
                if(form.AccountId.HasValue)
                {
                    _accountManager.UpdateAccount(form.MapToCore());
                }
                else
                {
                    _accountManager.AddAccount(form.MapToCore(), form.ParentId.Value);
                }
            }

            return Overview();
        }

        [HttpGet]
        public IActionResult GetAccountListing()
        {
            var accounts = _accountManager.GetHierarchy();

            var response = new List<AccountListing>();
            foreach (var account in accounts)
            {
                response.Add(AccountListing.MapFromObject(account));
            }

            return Ok(response);
        }

        [HttpGet("overview")]
        public IActionResult Overview()
        {
            var accounts = _accountManager.GetHierarchy();
            accounts = _accountManager.PopulateHierarchyExpeditures(accounts);
            accounts = _accountManager.PopulateHierarchyTransfers(accounts);

            var response = new List<AccountOverview>();
            foreach (var account in accounts)
            {
                response.Add(AccountOverview.MapFromObject(account));
            }

            return Ok(response);
        }

        [HttpGet("city")]
        public IActionResult GetCityAccoutns()
        {
            var accounts = _accountManager.GetCityAccounts();

            var response = new List<CityAccountListing>();
            foreach (var account in accounts)
            {
                response.Add(CityAccountListing.MapFromObject(account));
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
