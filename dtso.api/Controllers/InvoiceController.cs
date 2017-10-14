using dtso.api.Models.Responses;
using dtso.api.Utilities;
using dtso.core.Managers.Interfaces;
using dtso.core.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dtso.api.Controllers
{
    [Route("api/invoice")]
    public class InvoiceController : Controller
    {
        private IInvoiceManager _invoiceManager;
        ResponseGenerator _responseGenerator;

        public InvoiceController(IInvoiceManager invoiceMan)
        {
            _invoiceManager = invoiceMan;
            _responseGenerator = new ResponseGenerator();
        }

        [HttpGet("{invoiceId}")]
        public IActionResult GetInvoiceDetails(int invoiceId)
        {
            var invoice = _invoiceManager.GetInvoice(invoiceId);

            var response = InvoiceDetails.MapFromObject(invoice);

            return Ok(response);
        }

        [HttpGet("account/{accountNumber}")]
        public IActionResult InvoicesForAccounts(string accountNumber)
        {

            var parsedAccountNumber = new AccountNumberTemplate(accountNumber);
            if (!parsedAccountNumber.IsValid())
                return BadRequest("Account Number is invalid");

            var invoices = _invoiceManager.GetInvoicesForAccount(parsedAccountNumber);
            var response = new List<InvoiceBasic>();
            foreach (var invoice in invoices)
            {
                var flattenedInvoices = _responseGenerator.GenerateBasicInvoices(invoice);
                response = response.Concat(flattenedInvoices).ToList();
            }

            return Ok(response);
        }
    }
}
