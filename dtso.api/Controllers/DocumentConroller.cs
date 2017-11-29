using dtso.core.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dtso.api.Controllers
{
    [Route("api/document")]
    [Authorize]
    public class DocumentConroller : Controller
    {
        private WordDocumentHandle _wordDocument;

        public DocumentConroller(WordDocumentHandle wordDocument)
        {
            _wordDocument = wordDocument;
        }

        [HttpGet("coversheet/invoice/{invoiceid}")]
        public IActionResult CreateInvoice(int invoiceId)
        {
            _wordDocument.WriteSingleInvoiceCoversheet(invoiceId);

            return Ok();
        }
    }
}
