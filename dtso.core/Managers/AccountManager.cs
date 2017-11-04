﻿using dtso.core.Enums;
using dtso.core.Managers.Interfaces;
using dtso.core.Models;
using dtso.data.Repositories.Interfaces;
using dtso.data.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace dtso.core.Managers
{
    public class AccountManager : IAccountManager
    {
        private IAccountRepository _accountRepository;
        private IInvoiceManager _invoiceManager;
        private TicketManager _ticketManager;

        public AccountManager(IAccountRepository accountRepo, IInvoiceManager invoiceManager, TicketManager ticketManager)
        {
            _accountRepository = accountRepo;
            _invoiceManager = invoiceManager;
            _ticketManager = ticketManager;
        }

        public List<Account> PopulateExpeditures(List<Account> accounts)
        {
            foreach(var account in accounts)
            {
                account.ChildAccounts = PopulateExpeditures(account.ChildAccounts);

                //This could be generated by adding up the expeditures of all the childern plus the expeditures for this account. This works for now. 
                account.ExpedituresToDate = GetExpedituresToDate(new AccountNumberTemplate(account));
            }

            return accounts;
        } 

        public decimal GetExpedituresToDate(AccountNumberTemplate accountNumber)
        {
            decimal expenditures = 0;

            foreach(var invoice in _invoiceManager.GetInvoicesForAccount(accountNumber))
            {
                foreach(var accountExpense in invoice.AccountTotals)
                {
                    expenditures += accountExpense.Expense;
                }
            }

            foreach(var ticket in _ticketManager.GetTicketsForAccount(accountNumber, true))
            {
                expenditures += ticket.Cost;
            }

            return expenditures;
        }

        /// <summary>
        /// Retreives the complete Hierarchy (All roots and children)
        public List<Account> GetHierarchy()
        {
            var accountViews = _accountRepository.GetRootAccounts();

            List<Account> accounts = new List<Account>();
            foreach (var accountView in accountViews)
            {
                var account = _getAccountWithChildren(accountView);
                accounts.Add(account);
            }

            return accounts;
        }

        public List<CityAccount> GetCityAccounts()
        {
            var cityAccounts = new List<CityAccount>();

            foreach(var cityAccount in _accountRepository.GetCityAccounts())
            {
                cityAccounts.Add(CityAccount.MapFromEntity(cityAccount));
            }

            return cityAccounts;
        }

        public Account GetAccountDetails(AccountNumberTemplate accountNumber)
        {
            var account = Account.MapFromObject(_accountRepository.Get(accountNumber.AccountNumber.Value, accountNumber.SubNo, accountNumber.ShredNo));

            if (account == null)
                return null;

            var monthlyBreakdown = _initiateMonthlyBreakdown();
            monthlyBreakdown = _addInvoicesToMonthlyBreakdown(accountNumber, monthlyBreakdown); //Invoice
            monthlyBreakdown = _addTicketsToMonthlyBreakdown(accountNumber, monthlyBreakdown); //Ticket

            account.MonthlyDetails = monthlyBreakdown;
            account.ExpedituresToDate = _getMonthlyBreakdownTotalExpense(account.MonthlyDetails);

            return account;

        }


        /// <summary>
        /// Retrives the children of the provided account. Returns an empty list if it has not children
        /// </summary>
        /// <param name="accountView"></param>
        /// <returns></returns>
        private Account _getAccountWithChildren(vAccount accountView)
        {
            var account = Account.MapFromObject(accountView);
            
            var childAccounts = _accountRepository.GetChildAccounts(accountView);

            foreach (var childAccount in childAccounts)
            {
                var subAccount = _getAccountWithChildren(childAccount);
                account.ChildAccounts.Add(subAccount);
            }

            return account;
        }

        private Dictionary<int, MonthlyBreakdown> _initiateMonthlyBreakdown()
        {
            var breakdown = new Dictionary<int, MonthlyBreakdown>();

            foreach (int month in Enum.GetValues(typeof(Months)))
            {
                breakdown[month] = new MonthlyBreakdown(month); 
            }

            return breakdown;
        }

        private Dictionary<int, MonthlyBreakdown> _addInvoicesToMonthlyBreakdown(AccountNumberTemplate accountNumber, Dictionary<int, MonthlyBreakdown> monthlyBreakdown)
        {
            var invoices = _invoiceManager.GetInvoicesForAccount(accountNumber);
            foreach (var invoice in invoices)
            {
                var invoiceMonth = invoice.InvoiceDate.Month;
                var currentBreakdown = monthlyBreakdown[invoiceMonth];

                foreach (var accountExpense in invoice.AccountTotals)
                {
                    currentBreakdown.TotalExpense += accountExpense.Expense;
                }

                currentBreakdown.Invoices.Add(invoice);
                monthlyBreakdown[invoiceMonth] = currentBreakdown;
            }

            return monthlyBreakdown;
        }

        private Dictionary<int, MonthlyBreakdown> _addTicketsToMonthlyBreakdown(AccountNumberTemplate accountNumber, Dictionary<int, MonthlyBreakdown> monthlyBreakdown)
        {
            var tickets = _ticketManager.GetTicketsForAccount(accountNumber);
            foreach (var ticket in tickets)
            {
                var ticketMonth = ticket.Date.Month;
                var currentBreakdown = monthlyBreakdown[ticketMonth];

                //If ticket invoice is not null, then the expense was already counted with the incvoices
                if(ticket.Invoice == null)
                    currentBreakdown.TotalExpense += ticket.Cost;

                currentBreakdown.Tickets.Add(ticket);
                monthlyBreakdown[ticketMonth] = currentBreakdown;
            }

            return monthlyBreakdown;
        }

        private decimal _getMonthlyBreakdownTotalExpense(Dictionary<int, MonthlyBreakdown> monthlyBreakdown)
        {
            decimal total = 0;

            foreach(var month in monthlyBreakdown.Keys)
            {
                total += monthlyBreakdown[month].TotalExpense;
            }

            return total;
        }
    }
}
