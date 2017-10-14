using dtso.core.Managers;
using dtso.core.Managers.Interfaces;
using dtso.core.unittests._TestData;
using dtso.data.Repositories.Interfaces;
using dtso.data.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace dtso.core.unittests.Managers
{
    [TestClass]
    public class AccountManagerTests
    {
        vAccounts_TestData testData;

        Mock<IAccountRepository> accountRepositoryMock;
        Mock<IInvoiceManager> invoiceManagerMock;
        AccountManager accountManager;
        
        [TestInitialize]
        public void TestInit()
        {
            testData = new vAccounts_TestData();

            accountRepositoryMock = new Mock<IAccountRepository>();
            invoiceManagerMock = new Mock<IInvoiceManager>();

            accountRepositoryMock.Setup(repo => repo.GetRootAccounts())
                .Returns(testData.RootAccounts())
                .Verifiable();

            accountRepositoryMock.Setup(repo => repo.GetChildAccounts(It.IsAny<vAccount>()))
                .Returns((vAccount account) => testData.ChildAccounts(account))
                .Verifiable();

            accountManager = new AccountManager(accountRepositoryMock.Object, invoiceManagerMock.Object);
        }

        [TestMethod]
        [Description("Verified AccountManager.GetOverview() calls the appropriate repository calls")]
        public void GetOverview_RepositoryCalled()
        {
            accountManager.GetHierarchy();

            accountRepositoryMock.Verify(mock => mock.GetRootAccounts());
            accountRepositoryMock.Verify(mock => mock.GetChildAccounts(It.IsAny<vAccount>()));
        }

        [TestMethod]
        [Description("Verified that Root Level Ascoutns are AccountManager.GetHierarchy()")]
        public void GetOverview_RootsCreated()
        {
            var accounts = accountManager.GetHierarchy();

            Assert.IsTrue(accounts.Count > 0);
        }

        [TestMethod]
        [Description("Verified that Subaccounts are AccountManager.GetHierarchy()")]
        public void GetOverview_SubaccountsCreated()
        {
            var accounts = accountManager.GetHierarchy();

            foreach(var account in accounts)
            {
                Assert.IsTrue(account.ChildAccounts.Count > 0);
            }
        }

        [TestMethod]
        [Description("Verified that Shred Out Accounts are AccountManager.GetHierarchy()")]
        public void GetOverview_ShredacountsCreated()
        {
            var accounts = accountManager.GetHierarchy();

            foreach (var account in accounts)
            {
                foreach(var subaccount in account.ChildAccounts)
                {
                    Assert.IsTrue(subaccount.ChildAccounts.Count > 0);
                }
            }
        }
    }
}
