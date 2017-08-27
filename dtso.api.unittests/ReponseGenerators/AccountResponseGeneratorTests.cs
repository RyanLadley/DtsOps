using dtso.api.Models.Responses;
using dtso.api.ResponseGenerators;
using dtso.api.unittests._TestData;
using dtso.core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace dtso.api.unittests.ReponseGenerators
{
    [TestClass]
    public class AccountResponseGeneratorTests
    {
        AccountCoreServices_TestData account_data;
        AccountResponseGenerator responseGenerator;

        [TestInitialize]
        public void TestInit()
        {
            account_data = new AccountCoreServices_TestData();
            responseGenerator = new AccountResponseGenerator();
        }

        [TestMethod]
        [Description("Tests the proper mapping from a core.Services Account")]
        public void GenerateOverview_AccountCoreServices_Single()
        {
            var account = account_data.SingleAccount();

            var overview = responseGenerator.GenerateOverview(account);

            _verifyProperMapping(account, overview);
            Assert.IsTrue(overview.Subaccounts.Count == 0);
            
        }

        [TestMethod]
        [Description("Tests the proper mapping from a core.Services Account and all it's subaccounts")]
        public void GenerateOverview_AccountCoreServices_SingleAccountWithChildren()
        {
            var account = account_data.SingleAccountWithChildren();

            var overview = responseGenerator.GenerateOverview(account);

            _verifyProperMapping(account, overview);

            var accountAndOverviews = account.Subaccounts.Zip(overview.Subaccounts, (a, o) => new { Account = a, Overview = o });
            foreach(var mapped in accountAndOverviews)
            {
                _verifyProperMapping(mapped.Account, mapped.Overview);
            }
        }

        /// <summary>
        /// Verifies that the mapping from core.Services Account produces the desired result
        /// </summary>
        private void _verifyProperMapping(Account account, AccountOverview overview)
        {
            Assert.AreEqual(account.AccountNumber, overview.AccountNumber);
            Assert.AreEqual(account.SubNo, overview.SubNo);
            Assert.AreEqual(account.ShredNo, overview.ShredNo);
            Assert.AreEqual(account.Description, overview.Description);
            Assert.AreEqual(account.AnnualBudget, overview.AnnualBudget);
            Assert.AreEqual(account.AccountNumber, overview.AccountNumber);

            //These Fields Don't Map
            Assert.IsTrue(overview.GetType().GetProperty("AccountId") == null);
            Assert.IsTrue(overview.GetType().GetProperty("FundNumber") == null);
            Assert.IsTrue(overview.GetType().GetProperty("ProjectNumber") == null);
        }
    }
}
