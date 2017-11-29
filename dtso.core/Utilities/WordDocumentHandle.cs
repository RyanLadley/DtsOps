using dtso.core.Managers;
using dtso.core.Managers.Interfaces;
using dtso.core.Models;
using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace dtso.core.Utilities
{
    public class WordDocumentHandle
    {
        private IInvoiceManager _invoiceManager;
        private string _basePath;

        public WordDocumentHandle(IInvoiceManager invoiceManager)
        {
            _basePath = "wwwroot";
            _invoiceManager = invoiceManager;
        }

        public string WriteSingleInvoiceCoversheet(int invoiceId)
        {
            var invoice = _invoiceManager.GetInvoice(invoiceId);


            var coversheetPath = $"{_basePath}/test.docx";
            using (var fs = new FileStream(coversheetPath, FileMode.Create, FileAccess.Write))
            {
                XWPFDocument document = new XWPFDocument();
                document = _addSingleInvoiceTitle(document);
                document = _addSingleInvoiceInfo(document, invoice);
                document.Write(fs);
            }

            return coversheetPath;
        }

        private XWPFDocument _addSingleInvoiceTitle(XWPFDocument document)
        {
            var paragraph = document.CreateParagraph();
            paragraph.Alignment = ParagraphAlignment.CENTER;
            
            var run = paragraph.CreateRun();
            run.SetText("PPRTA Invoice Cover Sheet");
            run.AddCarriageReturn();
            run.AppendText("Public Works- Operations and Maintenance Division");

            return document;

        }

        private XWPFDocument _addSingleInvoiceInfo(XWPFDocument document, Invoice invoice)
        {
            var paragraph1 = document.CreateParagraph();
            paragraph1.Alignment = ParagraphAlignment.LEFT;

            paragraph1 = _addLabelRun(paragraph1, "Invoice No:");
            paragraph1 = _addValueRun(paragraph1, invoice.InvoiceNumber);
            paragraph1 = _addLabelRun(paragraph1, "\tfor");
            paragraph1 = _addValueRun(paragraph1, invoice.Vendor.Name);

            var paragraph2 = document.CreateParagraph();
            paragraph2.Alignment = ParagraphAlignment.LEFT;

            paragraph2 = _addLabelRun(paragraph2, "PPRTA Contract/PO No.:");
            paragraph2 = _addValueRun(paragraph2, invoice.Vendor.ContractNumber);

            var paragraph3 = document.CreateParagraph();
            paragraph3.Alignment = ParagraphAlignment.LEFT;

            paragraph3 = _addLabelRun(paragraph3, "Description:");
            paragraph3 = _addValueRun(paragraph3, invoice.Description);

            return document;
        }

        private XWPFParagraph _addLabelRun(XWPFParagraph paragraph, string text)
        {
            var run = paragraph.CreateRun();
            run.IsBold = true;
            run.SetText(text);
            run.AddTab();
            return paragraph;
        }



        private XWPFParagraph _addValueRun(XWPFParagraph paragraph, string text)
        {
            var run = paragraph.CreateRun();
            run.SetUnderline(UnderlinePatterns.Single);
            run.AddTab();
            run.SetText(text);
            run.AddTab();
            return paragraph;
        }
    }
}
