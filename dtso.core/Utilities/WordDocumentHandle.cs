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
                document.CreateParagraph();
                document = _addSingleInvoiceAccountsTable(document, invoice);
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


        private XWPFDocument _addSingleInvoiceAccountsTable(XWPFDocument document, Invoice invoice)
        {
            var table = document.CreateTable(1,4);
            table.Width = 4 * 1260;

            table.SetColumnWidth(0, 5);
            table.SetColumnWidth(1, 6);
            table.SetColumnWidth(2, 3);
            table.SetColumnWidth(3, 3);

            table.SetInsideHBorder(XWPFTable.XWPFBorderType.NIL, 0, 0, "white");
            table.SetInsideVBorder(XWPFTable.XWPFBorderType.NIL, 0, 0, "white");
            table.SetTopBorder(XWPFTable.XWPFBorderType.NIL, 0, 0, "white");
            table.SetLeftBorder(XWPFTable.XWPFBorderType.NIL, 0, 0, "white");
            table.SetRightBorder(XWPFTable.XWPFBorderType.NIL, 0, 0, "white");
            table.SetBottomBorder(XWPFTable.XWPFBorderType.NIL, 0,0, "white");

            var header = table.GetRow(0);
            _createTableCell(header.GetCell(0), "Project or Program", ParagraphAlignment.LEFT, true);
            _createTableCell(header.GetCell(1), "City Account Code", ParagraphAlignment.LEFT, true);
            _createTableCell(header.GetCell(2), "PPRTA Account Code", ParagraphAlignment.LEFT, true);
            _createTableCell(header.GetCell(3), "Amount", ParagraphAlignment.RIGHT, true);

            decimal total = 0;
            foreach(var account in invoice.AccountTotals)
            {
                foreach (var cityExpense in account.CityExpenses)
                {
                    var row = table.CreateRow();
                    _createTableCell(row.GetCell(0), account.Account.ProjectDescription, ParagraphAlignment.LEFT, false);
                    _createTableCell(row.GetCell(1), $"{cityExpense.CityAccount.CityAccountNumber}-{account.Account.FundNumber}-{account.Account.DeptartmentNumber}-{account.Account.ProjectNumber}", ParagraphAlignment.LEFT, false);
                    _createTableCell(row.GetCell(2), $"{account.Account.AccountPrefix}-{account.Account.AccountNumber}", ParagraphAlignment.LEFT, false);
                    _createTableCell(row.GetCell(3), string.Format("{0:C}", cityExpense.Expense), ParagraphAlignment.RIGHT, false);
                    total += cityExpense.Expense;
                }
            }

            var totalRow = table.CreateRow();
            _createTableCell(totalRow.GetCell(2), "Total", ParagraphAlignment.RIGHT, true);
            _createTableCell(totalRow.GetCell(3), string.Format("{0:C}",total), ParagraphAlignment.RIGHT, true);

            return document;
        }

        private XWPFTableCell _createTableCell(XWPFTableCell cell, string text, ParagraphAlignment alignment, bool isBold)
        {
            var paragraph = cell.Paragraphs[0];
            paragraph.Alignment = alignment;
            paragraph.SpacingAfterLines = 0;
            var run = paragraph.CreateRun();
            run.SetText(text);
            run.IsBold = isBold;
            return cell;
        }
    }
}
