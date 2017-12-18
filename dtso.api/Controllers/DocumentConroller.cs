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
        private SpreadsheetDocumentHandle _spreadsheet;

        public DocumentConroller(WordDocumentHandle wordDocument, SpreadsheetDocumentHandle spreadsheet)
        {
            _wordDocument = wordDocument;
            _spreadsheet = spreadsheet;
        }

        //[HttpGet("coversheet/invoice/{invoiceid}")]
        public IActionResult CreateInvoiceAsync(int invoiceId)
        {
            var filePath =  _wordDocument.WriteSingleInvoiceCoversheet(invoiceId);


            return Ok(filePath);
            /*var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(filePath), Path.GetFileName(filePath));*/
        }

        //[HttpGet("backup")]
        [HttpGet("coversheet/invoice/{invoiceid}")]
        public IActionResult CreateBackupSpreadsheet(int invoiceId)
        {
            var filePath = _spreadsheet.WriteBackupSpreadsheet();


            return Ok(filePath);
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
    }
}
