using dtso.core.Managers.Interfaces;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using dtso.core.Models;

namespace dtso.core.Utilities
{
    public class SpreadsheetDocumentHandle
    {
        private IInvoiceManager _invoiceManager;
        private IAccountManager _accountManager;
        private string _basePath;

        public SpreadsheetDocumentHandle(IInvoiceManager invoiceManager, IAccountManager accountManager)
        {
            _basePath = "StaticDocuments";
            _invoiceManager = invoiceManager;
            _accountManager = accountManager;
        }

        public string WriteBackupSpreadsheet()
        {
            var spreadsheetPath = $"Backup-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";

            var accounts = _accountManager.GetHierarchy();
            accounts = _accountManager.PopulateHierarchyExpeditures(accounts);
            accounts = _accountManager.PopulateHierarchyTransfers(accounts);

            IWorkbook workbook = new XSSFWorkbook();
            _addSummarySheet(workbook, accounts);

            foreach(var account in accounts)
            {
                _addAccountSheet(workbook, account);
            }

            using(var fileStream = new FileStream($"{_basePath}/{spreadsheetPath}", FileMode.Create, FileAccess.Write))
            {
                workbook.Write(fileStream);
            }

            return spreadsheetPath;
        }

        private void _addSummarySheet(IWorkbook workbook, List<Models.Account> accounts)
        {
            var summarySheet = workbook.CreateSheet("Summary");

            var headerRow = summarySheet.CreateRow(0);

            headerRow.CreateCell(0).SetCellValue("Account Code");
            headerRow.CreateCell(1).SetCellValue("Sub Account");
            headerRow.CreateCell(2).SetCellValue("Description");
            headerRow.CreateCell(3).SetCellValue("2018 Budget");
            headerRow.CreateCell(4).SetCellValue("Misc Transfer");
            headerRow.CreateCell(5).SetCellValue("Total Budget");
            headerRow.CreateCell(6).SetCellValue("Expenditures to Date");
            headerRow.CreateCell(7).SetCellValue("Remaining Balance");

            //Add style to all cells
            var headerStyle = workbook.CreateCellStyle();
            headerStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.BlueGrey.Index;
            headerStyle.FillPattern = FillPattern.SolidForeground;

            var headerFont = workbook.CreateFont();
            headerFont.Color = NPOI.HSSF.Util.HSSFColor.White.Index;

            headerStyle.SetFont(headerFont);
            foreach (var headerCell in headerRow.Cells)
            {
                headerCell.CellStyle = headerStyle;
            }

            IRow row;
            int currentRow = 1;
            ICell cell;

            var accountNumberStyle = workbook.CreateCellStyle();
            accountNumberStyle.Alignment = HorizontalAlignment.Left;
            var accountNumberFont = workbook.CreateFont();
            accountNumberFont.IsBold = true;
            accountNumberStyle.SetFont(accountNumberFont);

            var shredNumberSyle = workbook.CreateCellStyle();
            shredNumberSyle.Alignment = HorizontalAlignment.Center;


            foreach (var account in accounts)
            {
                row = summarySheet.CreateRow(currentRow);
                cell = row.CreateCell(0);
                cell.SetCellValue(account.AccountNumber);
                cell.CellStyle = accountNumberStyle;

                _populateAccountSummaryRow(workbook, row, account, currentRow);

                currentRow++;
                foreach(var subAccount in account.ChildAccounts)
                {
                    row = summarySheet.CreateRow(currentRow);
                    cell = row.CreateCell(1);
                    cell.SetCellValue($"{subAccount.AccountNumber}-{subAccount.SubNo}");
                    cell.CellStyle = accountNumberStyle;
                    _populateAccountSummaryRow(workbook, row, subAccount, currentRow);

                    currentRow++;
                    foreach(var shredAccount in subAccount.ChildAccounts)
                    {
                        row = summarySheet.CreateRow(currentRow);
                        cell = row.CreateCell(1);
                        cell.SetCellValue($"{shredAccount.AccountNumber}-{shredAccount.SubNo}-{shredAccount.ShredNo}");
                        cell.CellStyle = shredNumberSyle;
                        _populateAccountSummaryRow(workbook, row, shredAccount, currentRow);
                        currentRow++;
                    }
                }
            }
        }


        private void _populateAccountSummaryRow(IWorkbook workbook, IRow row, Models.Account account, int rowNumber)
        {
            rowNumber++; // Rows in exccell start at 1... losers

            ICell cell;
            var format = workbook.CreateDataFormat();
            var moneyStyle = workbook.CreateCellStyle();
            moneyStyle.DataFormat = format.GetFormat("$#,##0.00");

            cell = row.CreateCell(2);
            cell.SetCellValue(account.Description);

            cell = row.CreateCell(3);
            cell.SetCellValue(System.Convert.ToDouble(account.AnnualBudget));
            cell.CellStyle = moneyStyle;

            cell = row.CreateCell(4);
            cell.SetCellValue(System.Convert.ToDouble(account.Transfers));
            cell.CellStyle = moneyStyle;

            //Total Budget = Annual Budget(D) - Transfers(E) 
            var totalBudgetFormula = $"D{rowNumber}-E{rowNumber}";
            cell = row.CreateCell(5);
            cell.CellFormula = totalBudgetFormula;
            cell.CellStyle = moneyStyle;

            cell = row.CreateCell(6);
            cell.SetCellValue(System.Convert.ToDouble(account.ExpedituresToDate));
            cell.CellStyle = moneyStyle;

            //Remaining = TotalBudget(5) - Expeditures(6);
            var remainingBalanceFormula = $"F{rowNumber}-G{rowNumber}";
            cell = row.CreateCell(7);
            cell.CellFormula = remainingBalanceFormula;
            cell.CellStyle = moneyStyle;
        }


        private void _addAccountSheet(IWorkbook workbook, Account account)
        {
            var accountNumber = new AccountNumberTemplate(account);
            account = _accountManager.GetAccountDetails(accountNumber);

            var accountSheet = workbook.CreateSheet(accountNumber.stringifyAccountNumber());

            ICell cell;

            var headerStyle = workbook.CreateCellStyle();
            headerStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Green.Index;
            headerStyle.FillPattern = FillPattern.SolidForeground;

            var headerRow = accountSheet.CreateRow(0);
            cell = headerRow.CreateCell(0);
            cell.SetCellValue($"{accountNumber.stringifyAccountNumber()}-{account.Description}");
            cell.CellStyle = headerStyle;
            accountSheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, 6));

            accountSheet.CreateRow(1);
            _addInvoiceHeader(workbook, accountSheet, 2);

            //The last month is an emtpy string, so be sure to take that into account
            var months = (new System.Globalization.CultureInfo("en-US")).DateTimeFormat.MonthNames;

            var rowNumber = 3;
            for(var i = 0; i < months.Length-1; i++)
            {
                rowNumber = _addMonthlyInvoices(workbook, accountSheet, rowNumber, account, i, months[i]);
            }

        }
        private void _addInvoiceHeader(IWorkbook workbook, ISheet accountSheet, int rowNumber)
        {
            ICell cell;
            var invoiceHeader = accountSheet.CreateRow(rowNumber);

            var headerStyle = workbook.CreateCellStyle();
            headerStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Gold.Index;
            headerStyle.FillPattern = FillPattern.SolidForeground;
            headerStyle.BorderBottom = BorderStyle.Thin;
            headerStyle.BorderTop = BorderStyle.Thin;
            headerStyle.BorderLeft = BorderStyle.Thin;
            headerStyle.BorderRight = BorderStyle.Thin;

            cell = invoiceHeader.CreateCell(0);
            cell.SetCellValue("Account#");
            cell.CellStyle = headerStyle;

            cell = invoiceHeader.CreateCell(1);
            cell.SetCellValue("Vendor");
            cell.CellStyle = headerStyle;

            cell = invoiceHeader.CreateCell(2);
            cell.SetCellValue("Invoice Date");
            cell.CellStyle = headerStyle;

            cell = invoiceHeader.CreateCell(3);
            cell.SetCellValue("Date Paid");
            cell.CellStyle = headerStyle;

            cell = invoiceHeader.CreateCell(4);
            cell.SetCellValue("Invoice Number");
            cell.CellStyle = headerStyle;

            cell = invoiceHeader.CreateCell(5);
            cell.SetCellValue("Description");
            cell.CellStyle = headerStyle;

            cell = invoiceHeader.CreateCell(6);
            cell.SetCellValue("Total Expensed");
            cell.CellStyle = headerStyle;
        }

        private int _addMonthlyInvoices(IWorkbook workbook, ISheet accountSheet, int rowNumber, Account account,  int monthIndex, string monthName)
        {
            ICell cell;
            var startRow = rowNumber;
            var minRows = 30;

            var headerStyle = workbook.CreateCellStyle();
            headerStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Maroon.Index;
            headerStyle.FillPattern = FillPattern.SolidForeground;
            headerStyle.Alignment = HorizontalAlignment.Center;
            headerStyle.BorderBottom = BorderStyle.Thin;
            headerStyle.BorderTop = BorderStyle.Thin;
            headerStyle.BorderLeft = BorderStyle.Thin;
            headerStyle.BorderRight = BorderStyle.Thin;


            var headerRange = new NPOI.SS.Util.CellRangeAddress(rowNumber, rowNumber, 0, 6);
            accountSheet.AddMergedRegion(headerRange);
            var headerRow = accountSheet.CreateRow(rowNumber);
            cell = headerRow.CreateCell(0);
            cell.SetCellValue($"{monthName}");
            cell.CellStyle = headerStyle;
            cell = headerRow.CreateCell(6);
            cell.SetCellValue($"{monthName}");
            cell.CellStyle = headerStyle;


            var format = workbook.CreateDataFormat();
            var moneyStyle = workbook.CreateCellStyle();
            moneyStyle.DataFormat = format.GetFormat("$#,##0.00");
            moneyStyle.BorderBottom = BorderStyle.Thin;
            moneyStyle.BorderTop = BorderStyle.Thin;
            moneyStyle.BorderLeft = BorderStyle.Thin;
            moneyStyle.BorderRight = BorderStyle.Thin;

            var dateStyle = workbook.CreateCellStyle();
            dateStyle.DataFormat = format.GetFormat("m-d-yyyy");
            dateStyle.BorderBottom = BorderStyle.Thin;
            dateStyle.BorderTop = BorderStyle.Thin;
            dateStyle.BorderLeft = BorderStyle.Thin;
            dateStyle.BorderRight = BorderStyle.Thin;

            var normalStyle = workbook.CreateCellStyle();
            normalStyle.BorderBottom = BorderStyle.Thin;
            normalStyle.BorderTop = BorderStyle.Thin;
            normalStyle.BorderLeft = BorderStyle.Thin;
            normalStyle.BorderRight = BorderStyle.Thin;

            rowNumber++;
            IRow row;
            foreach(var invoice in account.MonthlyDetails[monthIndex+1].Invoices)
            {
                foreach(var invoiceAccount in invoice.AccountTotals)
                {
                    row = accountSheet.CreateRow(rowNumber);

                    cell=row.CreateCell(0);
                    cell.SetCellValue(new AccountNumberTemplate(invoiceAccount.Account).stringifyAccountNumber());
                    cell.CellStyle = normalStyle;

                    cell = row.CreateCell(1);
                    cell.SetCellValue(invoice.Vendor.Name);
                    cell.CellStyle = normalStyle;

                    cell = row.CreateCell(2);
                    cell.SetCellValue(invoice.InvoiceDate);
                    cell.CellStyle = dateStyle;

                    cell = row.CreateCell(3);
                    cell.SetCellValue(invoice.DatePaid);
                    cell.CellStyle = dateStyle;

                    cell = row.CreateCell(4);
                    cell.SetCellValue(invoice.InvoiceNumber);
                    cell.CellStyle = normalStyle;

                    cell = row.CreateCell(5);
                    cell.SetCellValue(invoice.Description);
                    cell.CellStyle = normalStyle;

                    cell = row.CreateCell(6);
                    cell.SetCellValue(System.Convert.ToDouble(invoiceAccount.Expense));
                    cell.CellStyle = moneyStyle;

                    rowNumber++;
                }
            }

            var usedRows = rowNumber - startRow;
            while (usedRows < minRows)
            {
                row = accountSheet.CreateRow(rowNumber);
                cell = row.CreateCell(0);
                cell.CellStyle = normalStyle;

                cell = row.CreateCell(1);
                cell.CellStyle = normalStyle;

                cell = row.CreateCell(2);
                cell.CellStyle = dateStyle;

                cell = row.CreateCell(3);
                cell.CellStyle = dateStyle;

                cell = row.CreateCell(4);
                cell.CellStyle = normalStyle;

                cell = row.CreateCell(5);
                cell.CellStyle = normalStyle;

                cell = row.CreateCell(6);
                cell.CellStyle = moneyStyle;

                rowNumber++;
                usedRows++;
            }

            var totalStyle = workbook.CreateCellStyle();
            totalStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Coral.Index;
            totalStyle.FillPattern = FillPattern.SolidForeground;
            totalStyle.BorderBottom = BorderStyle.Thin;
            totalStyle.BorderTop = BorderStyle.Thin;
            totalStyle.BorderRight = BorderStyle.None;

            var totalEndStyle = workbook.CreateCellStyle();
            totalEndStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Coral.Index;
            totalEndStyle.FillPattern = FillPattern.SolidForeground;
            totalEndStyle.DataFormat = format.GetFormat("$#,##0.00");
            totalEndStyle.BorderBottom = BorderStyle.Thin;
            totalEndStyle.BorderRight = BorderStyle.Thin;
            totalEndStyle.BorderTop = BorderStyle.Thin;

            var totalRow = accountSheet.CreateRow(rowNumber);
            cell = totalRow.CreateCell(0);
            cell.SetCellValue($"{monthName} Total");
            cell.CellStyle = totalStyle;

            cell = totalRow.CreateCell(1);
            cell.CellStyle = totalStyle;

            cell = totalRow.CreateCell(2);
            cell.CellStyle = totalStyle;

            cell = totalRow.CreateCell(3);
            cell.CellStyle = totalStyle;

            cell = totalRow.CreateCell(4);
            cell.CellStyle = totalStyle;

            cell = totalRow.CreateCell(5);
            cell.CellStyle = totalStyle;

            cell = totalRow.CreateCell(6);
            cell.CellFormula = $"SUM(G{startRow+2}:G{rowNumber})";
            cell.CellStyle = totalEndStyle;

            rowNumber++;


            return rowNumber;
        }
    }
}
