using dtso.core.Services;
using dtso.core.unittests._TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace dtso.core.unittests.Services
{
    [TestClass]
    public class AccountTests
    {
        vAccounts_TestData vAccount_data;
        

        [TestInitialize]
        public void TestInit()
        {
            vAccount_data = new vAccounts_TestData();
        }

        [TestMethod]
        public void MapFromObject_vAccount()
        {
            var account = vAccount_data.SingleAccount();

            var accountToTest = Account.MapFromObject(account);
            
            Assert.AreEqual(account.AccountNumber, accountToTest.AccountNumber);
            Assert.AreEqual(account.SubNo, accountToTest.SubNo);
            Assert.AreEqual(account.ShredNo, accountToTest.ShredNo);
            Assert.AreEqual(account.Description, accountToTest.Description);
            Assert.AreEqual(account.AnnualBudget, accountToTest.AnnualBudget);
            Assert.AreEqual(account.AccountNumber, accountToTest.AccountNumber);
            Assert.AreEqual(account.FundNumber, accountToTest.FundNumber);
            Assert.AreEqual(account.ProjectNumber, accountToTest.ProjectNumber);
            Assert.IsTrue(accountToTest.ChildAccounts.Count == 0);
        }
    }
}
