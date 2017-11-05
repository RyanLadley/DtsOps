using dtso.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dtso.api.Models.Responses
{
    public class InvoiceDetails
    {
        public int InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }

        public List<InvoiceExpense> Expenses { get; set; }
        public VendorListing Vendor { get; set; }
        public InvoiceTypeListing InvoiceType { get; set; }

        public DateTime InvoiceDate { get; set; }
        public DateTime DatePaid { get; set; }
        public string Description { get; set; }
        public decimal TotalExpense { get; set; }

        public List<TicketBasic> Tickets { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        public static InvoiceDetails MapFromObject(Invoice invoice)
        {
            var details = new InvoiceDetails()
            {
                InvoiceId = invoice.InvoiceId,
                InvoiceDate = invoice.InvoiceDate,
                DatePaid = invoice.DatePaid,
                InvoiceNumber = invoice.InvoiceNumber,
                InvoiceType = InvoiceTypeListing.MapFromObject(invoice.InvoiceType),
                Description = invoice.Description,
                Vendor = VendorListing.MapFromObject(invoice.Vendor)
            };

            details.Tickets = new List<TicketBasic>();
            foreach(var ticket in invoice.Tickets)
            {
                details.Tickets.Add(TicketBasic.MapFromObject(ticket));
            }


            decimal totalExpense = 0;
            details.Expenses = new List<InvoiceExpense>();
            foreach (var accountTotal in invoice.AccountTotals)
            {
                totalExpense += accountTotal.Expense;

                var cityExpenses = new List<CityExpenseBasic>();
                foreach(var expense in accountTotal.CityExpenses)
                {
                    cityExpenses.Add(CityExpenseBasic.MapFromObject(expense));
                }

                details.Expenses.Add(new InvoiceExpense()
                {
                    InvoiceAccountId = accountTotal.InvoiceAccountId,
                    Expense = accountTotal.Expense,
                    Account = AccountListing.MapFromObject(accountTotal.Account),
                    CityExpense = cityExpenses
                });
            }

            details.TotalExpense = totalExpense;
            return details;
        }

    }
}
