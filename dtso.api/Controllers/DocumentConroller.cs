using dtso.core.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace dtso.api.Controllers
{
    [Route("api/document")]
    [Authorize]
    public class DocumentConroller : Controller
    {
        private WordDocumentHandle _wordDocument;
        private BackupSpreadsheetHandle _spreadsheet;

        public DocumentConroller(WordDocumentHandle wordDocument, BackupSpreadsheetHandle spreadsheet)
        {
            _wordDocument = wordDocument;
            _spreadsheet = spreadsheet;
        }

        [HttpGet("coversheet/invoice/{invoiceid}")]
        public IActionResult CreateInvoiceAsync(int invoiceId)
        {
            var filePath =  _wordDocument.WriteSingleInvoiceCoversheet(invoiceId);


            return Ok(filePath);
        }

        [HttpGet("backup")]
        public IActionResult CreateBackupSpreadsheet(int invoiceId)
        {
            var filePath = _spreadsheet.WriteBackupSpreadsheet();


            return Ok(filePath);
        }
    }
}
