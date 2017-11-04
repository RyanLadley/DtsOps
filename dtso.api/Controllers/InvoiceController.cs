using dtso.api.Models.Forms;
using dtso.api.Models.Responses;
using dtso.api.Utilities;
using dtso.core.Managers.Interfaces;
using dtso.core.Models;
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

        [HttpPost]
        public IActionResult CreateInvoice([FromBody] InvoiceForm form)
        {
            var status = _invoiceManager.CreateInvoice(form.MapToCore());
            if (status == "SUCCESS")
                return Ok();
            else
                return BadRequest(status);
        }

        [HttpPost("edit")]
        public IActionResult EditInvoice([FromBody] InvoiceForm form)
        {
            var invoice = _invoiceManager.EditInvoice(form.MapToCore());
            if (invoice == null)
                return BadRequest("There Was An Error");
            else
            {
                var response = InvoiceDetails.MapFromObject(invoice);
                return Ok(response);
            }
        }

        [HttpGet("types")]
        public IActionResult GetInvoiceTypes()
        {
            var types = _invoiceManager.GetInvoiceTypes();


            var response = new List<InvoiceTypeListing>();
            foreach(var type in types)
            {
                response.Add(InvoiceTypeListing.MapFromObject(type));
            }

            return Ok(response);
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
