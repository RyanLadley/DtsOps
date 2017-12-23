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
    public class BackupSpreadsheetHandle
    {
        private IInvoiceManager _invoiceManager;
        private IAccountManager _accountManager;
        private string _basePath;
        private ICellStyle _moneyStyle;
        private ICellStyle _borderStyle;
        private ICellStyle _totalStyle;
        private ICellStyle _monthlyHeaderStyle;
        private ICellStyle _dateStyle;
        private ICellStyle _monthlyTotalStyle;
        private ICellStyle _monthlyTotalEndStyle;
        private IWorkbook _workbook;

        public BackupSpreadsheetHandle(IInvoiceManager invoiceManager, IAccountManager accountManager)
        {
            _basePath = "StaticDocuments";
            _invoiceManager = invoiceManager;
            _accountManager = accountManager;
        }

        public string WriteBackupSpreadsheet()
        {
            var spreadsheetPath = $"Backup-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";

            _workbook = new XSSFWorkbook();
            var accounts = _accountManager.GetHierarchy();
            accounts = _accountManager.PopulateHierarchyExpeditures(accounts);
            accounts = _accountManager.PopulateHierarchyTransfers(accounts);
            
            var format = _workbook.CreateDataFormat();
            _moneyStyle = _workbook.CreateCellStyle();
            _moneyStyle.DataFormat = format.GetFormat("$#,##0.00");
            _moneyStyle.BorderBottom = BorderStyle.Thin;
            _moneyStyle.BorderTop = BorderStyle.Thin;
            _moneyStyle.BorderLeft = BorderStyle.Thin;
            _moneyStyle.BorderRight = BorderStyle.Thin;

            _borderStyle = _workbook.CreateCellStyle();
            _borderStyle.BorderBottom = BorderStyle.Thin;
            _borderStyle.BorderTop = BorderStyle.Thin;
            _borderStyle.BorderLeft = BorderStyle.Thin;
            _borderStyle.BorderRight = BorderStyle.Thin;


            _totalStyle = _workbook.CreateCellStyle();
            _totalStyle.DataFormat = format.GetFormat("$#,##0.00");
            _totalStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Yellow.Index;
            _totalStyle.FillPattern = FillPattern.SolidForeground;
            _totalStyle.BorderBottom = BorderStyle.Thin;
            _totalStyle.BorderTop = BorderStyle.Thin;
            _totalStyle.BorderLeft = BorderStyle.Thin;
            _totalStyle.BorderRight = BorderStyle.Thin;

            //Monthly Summary Block Header
            _monthlyHeaderStyle = _workbook.CreateCellStyle();
            _monthlyHeaderStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Coral.Index;
            _monthlyHeaderStyle.Alignment = HorizontalAlignment.Center;
            _monthlyHeaderStyle.FillPattern = FillPattern.SolidForeground;
            _monthlyHeaderStyle.BorderLeft = BorderStyle.Thin;
            _monthlyHeaderStyle.BorderBottom = BorderStyle.Thin;
            _monthlyHeaderStyle.BorderRight = BorderStyle.Thin;
            _monthlyHeaderStyle.BorderTop = BorderStyle.Thin;


            _dateStyle = _workbook.CreateCellStyle();
            _dateStyle.DataFormat = format.GetFormat("m-d-yyyy");
            _dateStyle.BorderBottom = BorderStyle.Thin;
            _dateStyle.BorderTop = BorderStyle.Thin;
            _dateStyle.BorderLeft = BorderStyle.Thin;
            _dateStyle.BorderRight = BorderStyle.Thin;

            _monthlyTotalStyle = _workbook.CreateCellStyle();
            _monthlyTotalStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Coral.Index;
            _monthlyTotalStyle.FillPattern = FillPattern.SolidForeground;
            _monthlyTotalStyle.BorderBottom = BorderStyle.Thin;
            _monthlyTotalStyle.BorderTop = BorderStyle.Thin;
            _monthlyTotalStyle.BorderRight = BorderStyle.None;

            _monthlyTotalEndStyle = _workbook.CreateCellStyle();
            _monthlyTotalEndStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Coral.Index;
            _monthlyTotalEndStyle.FillPattern = FillPattern.SolidForeground;
            _monthlyTotalEndStyle.DataFormat = format.GetFormat("$#,##0.00");
            _monthlyTotalEndStyle.BorderBottom = BorderStyle.Thin;
            _monthlyTotalEndStyle.BorderRight = BorderStyle.Thin;
            _monthlyTotalEndStyle.BorderTop = BorderStyle.Thin;

            _addSummarySheet(accounts);

            foreach(var account in accounts)
            {
                _addAccountSheet(account);
                foreach(var subAccount in account.ChildAccounts)
                {
                    _addAccountSheet(subAccount);
                    foreach(var shredAcount in subAccount.ChildAccounts)
                    {
                      _addAccountSheet(shredAcount);
                    }
                }
            }

            using(var fileStream = new FileStream($"{_basePath}/{spreadsheetPath}", FileMode.Create, FileAccess.Write))
            {
                _workbook.Write(fileStream);
            }

            return spreadsheetPath;
        }

        private void _addSummarySheet(List<Models.Account> accounts)
        {
            var summarySheet = _workbook.CreateSheet("Summary");

            var row = summarySheet.CreateRow(0);

            row.CreateCell(0).SetCellValue("Account Code");
            row.CreateCell(1).SetCellValue("Sub Account");
            row.CreateCell(2).SetCellValue("Description");
            row.CreateCell(3).SetCellValue("2018 Budget");
            row.CreateCell(4).SetCellValue("Misc Transfer");
            row.CreateCell(5).SetCellValue("Total Budget");
            row.CreateCell(6).SetCellValue("Expenditures to Date");
            row.CreateCell(7).SetCellValue("Remaining Balance");

            //Add style to all cells
            var headerStyle = _workbook.CreateCellStyle();
            headerStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.BlueGrey.Index;
            headerStyle.FillPattern = FillPattern.SolidForeground;

            var headerFont = _workbook.CreateFont();
            headerFont.Color = NPOI.HSSF.Util.HSSFColor.White.Index;

            headerStyle.SetFont(headerFont);
            foreach (var headerCell in row.Cells)
            {
                headerCell.CellStyle = headerStyle;
            }

            int currentRow = 1;
            ICell cell;

            var accountNumberStyle = _workbook.CreateCellStyle();
            accountNumberStyle.Alignment = HorizontalAlignment.Left;
            var accountNumberFont = _workbook.CreateFont();
            accountNumberFont.IsBold = true;
            accountNumberStyle.SetFont(accountNumberFont);
            accountNumberStyle.BorderBottom = BorderStyle.Thin;
            accountNumberStyle.BorderTop = BorderStyle.Thin;
            accountNumberStyle.BorderLeft = BorderStyle.Thin;
            accountNumberStyle.BorderRight = BorderStyle.Thin;

            var shredNumberSyle = _workbook.CreateCellStyle();
            shredNumberSyle.Alignment = HorizontalAlignment.Center;
            shredNumberSyle.BorderBottom = BorderStyle.Thin;
            shredNumberSyle.BorderTop = BorderStyle.Thin;
            shredNumberSyle.BorderLeft = BorderStyle.Thin;
            shredNumberSyle.BorderRight = BorderStyle.Thin;


            foreach (var account in accounts)
            {
                row = summarySheet.CreateRow(currentRow);
                cell = row.CreateCell(0);
                cell.SetCellValue(account.AccountNumber);
                cell.CellStyle = accountNumberStyle;

                _populateAccountSummaryRow(row, account, currentRow);

                currentRow++;
                foreach(var subAccount in account.ChildAccounts)
                {
                    row = summarySheet.CreateRow(currentRow);
                    cell = row.CreateCell(1);
                    cell.SetCellValue($"{subAccount.AccountNumber}-{subAccount.SubNo}");
                    cell.CellStyle = accountNumberStyle;
                    _populateAccountSummaryRow(row, subAccount, currentRow);

                    currentRow++;
                    foreach(var shredAccount in subAccount.ChildAccounts)
                    {
                        row = summarySheet.CreateRow(currentRow);
                        cell = row.CreateCell(1);
                        cell.SetCellValue($"{shredAccount.AccountNumber}-{shredAccount.SubNo}-{shredAccount.ShredNo}");
                        cell.CellStyle = shredNumberSyle;
                        _populateAccountSummaryRow(row, shredAccount, currentRow);
                        currentRow++;
                    }
                }
            }
        }


        private void _populateAccountSummaryRow(IRow row, Models.Account account, int rowNumber)
        {
            rowNumber++; // Rows in exccell start at 1... losers

            ICell cell;

            cell = row.CreateCell(2);
            cell.SetCellValue(account.Description);
            cell.CellStyle = _borderStyle;

            cell = row.CreateCell(3);
            cell.SetCellValue(System.Convert.ToDouble(account.AnnualBudget));
            cell.CellStyle = _moneyStyle;

            cell = row.CreateCell(4);
            cell.SetCellValue(System.Convert.ToDouble(account.Transfers));
            cell.CellStyle = _moneyStyle;

            //Total Budget = Annual Budget(D) - Transfers(E) 
            var totalBudgetFormula = $"D{rowNumber}-E{rowNumber}";
            cell = row.CreateCell(5);
            cell.CellFormula = totalBudgetFormula;
            cell.CellStyle = _moneyStyle;

            cell = row.CreateCell(6);
            cell.SetCellValue(System.Convert.ToDouble(account.ExpedituresToDate));
            cell.CellStyle = _moneyStyle;

            //Remaining = TotalBudget(5) - Expeditures(6);
            var remainingBalanceFormula = $"F{rowNumber}-G{rowNumber}";
            cell = row.CreateCell(7);
            cell.CellFormula = remainingBalanceFormula;
            cell.CellStyle = _moneyStyle;
        }


        private void _addAccountSheet(Account account)
        {
            var accountNumber = new AccountNumberTemplate(account);
            account = _accountManager.GetAccountDetails(accountNumber);

            var accountSheet = _workbook.CreateSheet(accountNumber.stringifyAccountNumber());

            ICell cell;

            var headerStyle = _workbook.CreateCellStyle();
            headerStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Green.Index;
            headerStyle.FillPattern = FillPattern.SolidForeground;
            headerStyle.Alignment = HorizontalAlignment.Center;
            var headerFont = _workbook.CreateFont();
            headerFont.Color = NPOI.HSSF.Util.HSSFColor.White.Index;
            headerStyle.SetFont(headerFont);

            var row = accountSheet.CreateRow(0);
            cell = row.CreateCell(0);
            cell.SetCellValue($"{accountNumber.stringifyAccountNumber()}-{account.Description}");
            cell.CellStyle = headerStyle;
            accountSheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, 6));
            accountSheet.CreateRow(1);
            _addInvoiceHeader(ref accountSheet, 2);

            

            row = accountSheet.CreateRow(6);
            cell = row.CreateCell(8);
            cell.SetCellValue("Monthly Totals");
            cell.CellStyle = _monthlyHeaderStyle;
            cell = row.CreateCell(9);
            cell.CellStyle = _monthlyHeaderStyle;
            accountSheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(6, 6, 8, 9));

            row = accountSheet.CreateRow(7);
            cell = row.CreateCell(8);
            cell.SetCellValue("Month");
            cell = row.CreateCell(9);
            cell.SetCellValue("Actual");

            //The last month is an emtpy string, so be sure to take that into account
            var months = (new System.Globalization.CultureInfo("en-US")).DateTimeFormat.MonthNames;

            var rowNumber = 3;
            for(var i = 0; i < months.Length-1; i++)
            {
                rowNumber = _addMonthlyInvoices(ref accountSheet, rowNumber, account, i, months[i]);
                _addMonthlyAccountSummaryCell(ref accountSheet, rowNumber, i, months[i]);
            }

            _addAccountBudget(ref accountSheet, account);
            _addBudgetTotal(ref accountSheet);
        }

        private void _addBudgetTotal(ref ISheet accountSheet)
        {
            var row = accountSheet.GetRow(21);
            var cell = row.CreateCell(8);
            cell.SetCellValue("Total");
            cell.CellStyle = _borderStyle;

            cell = row.CreateCell(9);
            cell.CellFormula = $"SUM(J9:J21)";
            cell.CellStyle = _totalStyle;
        }

        private void _addAccountBudget(ref ISheet accountSheet, Account account)
        {

            var row = accountSheet.GetRow(0) ?? accountSheet.CreateRow(0);
            
            var cell = row.CreateCell(8);
            cell.SetCellValue("Budget");
            cell.CellStyle = _borderStyle;

            cell = row.CreateCell(9);
            cell.SetCellValue(System.Convert.ToDouble(account.AnnualBudget - account.Transfers));
            cell.CellStyle = _moneyStyle;

            row = accountSheet.GetRow(1) ?? accountSheet.CreateRow(1);

            cell = row.CreateCell(8);
            cell.SetCellValue("Expensed");
            cell.CellStyle = _borderStyle;

            cell = row.CreateCell(9);
            cell.CellFormula = "$J$22";
            cell.CellStyle = _moneyStyle;


            row = accountSheet.GetRow(2) ?? accountSheet.CreateRow(2);

            cell = row.CreateCell(8);
            cell.SetCellValue("Remaining");
            cell.CellStyle = _borderStyle;

            cell = row.CreateCell(9);
            cell.CellFormula = $"j1-j2";
            cell.CellStyle = _totalStyle;
        }

        private void _addMonthlyAccountSummaryCell(ref ISheet accountSheet, int totalRow, int index, string month)
        {
            var startRow = 8;

            var row = accountSheet.GetRow(startRow + index);
            var cell = row.CreateCell(8);
            cell.SetCellValue(month);
            cell.CellStyle = _borderStyle;

            cell = row.CreateCell(9);
            cell.CellFormula = $"$G${totalRow}";
            cell.CellStyle = _moneyStyle;
        }

        private void _addInvoiceHeader(ref ISheet accountSheet, int rowNumber)
        {
            ICell cell;
            var invoiceHeader = accountSheet.CreateRow(rowNumber);

            var headerStyle = _workbook.CreateCellStyle();
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

        private int _addMonthlyInvoices(ref ISheet accountSheet, int rowNumber, Account account,  int monthIndex, string monthName)
        {
            ICell cell;
            var startRow = rowNumber;
            var minRows = 30;

            var headerStyle = _workbook.CreateCellStyle();
            headerStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Maroon.Index;
            headerStyle.FillPattern = FillPattern.SolidForeground;
            headerStyle.Alignment = HorizontalAlignment.Center;
            headerStyle.BorderBottom = BorderStyle.Thin;
            headerStyle.BorderTop = BorderStyle.Thin;
            headerStyle.BorderLeft = BorderStyle.Thin;
            headerStyle.BorderRight = BorderStyle.Thin;
            var headerFont = _workbook.CreateFont();
            headerFont.Color = NPOI.HSSF.Util.HSSFColor.White.Index;
            headerStyle.SetFont(headerFont);

            var headerRange = new NPOI.SS.Util.CellRangeAddress(rowNumber, rowNumber, 0, 6);
            accountSheet.AddMergedRegion(headerRange);
            var row = accountSheet.CreateRow(rowNumber);
            cell = row.CreateCell(0);
            cell.SetCellValue($"{monthName}");
            cell.CellStyle = headerStyle;
            cell = row.CreateCell(6);
            cell.SetCellValue($"{monthName}");
            cell.CellStyle = headerStyle;


            

            rowNumber++;
            foreach(var invoice in account.MonthlyDetails[monthIndex+1].Invoices)
            {
                foreach(var invoiceAccount in invoice.AccountTotals)
                {
                    row = accountSheet.GetRow(rowNumber) ?? accountSheet.CreateRow(rowNumber);

                    cell=row.CreateCell(0);
                    cell.SetCellValue(new AccountNumberTemplate(invoiceAccount.Account).stringifyAccountNumber());
                    cell.CellStyle = _borderStyle;

                    cell = row.CreateCell(1);
                    cell.SetCellValue(invoice.Vendor.Name);
                    cell.CellStyle = _borderStyle;

                    cell = row.CreateCell(2);
                    cell.SetCellValue(invoice.InvoiceDate);
                    cell.CellStyle = _dateStyle;

                    cell = row.CreateCell(3);
                    cell.SetCellValue(invoice.DatePaid);
                    cell.CellStyle = _dateStyle;

                    cell = row.CreateCell(4);
                    cell.SetCellValue(invoice.InvoiceNumber);
                    cell.CellStyle = _borderStyle;

                    cell = row.CreateCell(5);
                    cell.SetCellValue(invoice.Description);
                    cell.CellStyle = _borderStyle;

                    cell = row.CreateCell(6);
                    cell.SetCellValue(System.Convert.ToDouble(invoiceAccount.Expense));
                    cell.CellStyle = _moneyStyle;

                    rowNumber++;
                }
            }

            var usedRows = rowNumber - startRow;
            while (usedRows < minRows)
            {
                row = accountSheet.GetRow(rowNumber) ?? accountSheet.CreateRow(rowNumber);
                cell = row.CreateCell(0);
                cell.CellStyle = _borderStyle;

                cell = row.CreateCell(1);
                cell.CellStyle = _borderStyle;

                cell = row.CreateCell(2);
                cell.CellStyle = _dateStyle;

                cell = row.CreateCell(3);
                cell.CellStyle = _dateStyle;

                cell = row.CreateCell(4);
                cell.CellStyle = _borderStyle;

                cell = row.CreateCell(5);
                cell.CellStyle = _borderStyle;

                cell = row.CreateCell(6);
                cell.CellStyle = _moneyStyle;

                rowNumber++;
                usedRows++;
            }



            row = accountSheet.CreateRow(rowNumber);
            cell = row.CreateCell(0);
            cell.SetCellValue($"{monthName} Total");
            cell.CellStyle = _monthlyTotalStyle;

            cell = row.CreateCell(1);
            cell.CellStyle = _monthlyTotalStyle;

            cell = row.CreateCell(2);
            cell.CellStyle = _monthlyTotalStyle;

            cell = row.CreateCell(3);
            cell.CellStyle = _monthlyTotalStyle;

            cell = row.CreateCell(4);
            cell.CellStyle = _monthlyTotalStyle;

            cell = row.CreateCell(5);
            cell.CellStyle = _monthlyTotalStyle;

            cell = row.CreateCell(6);
            cell.CellFormula = $"SUM(G{startRow+2}:G{rowNumber})";
            cell.CellStyle = _monthlyTotalEndStyle;

            rowNumber++;


            return rowNumber;
        }
    }
}
