using dtso.core.Managers;
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
        AccountManager accountManager;
        
        [TestInitialize]
        public void TestInit()
        {
            testData = new vAccounts_TestData();

            accountRepositoryMock = new Mock<IAccountRepository>();

            accountRepositoryMock.Setup(repo => repo.GetRootAccounts())
                .Returns(testData.RootAccounts())
                .Verifiable();

            accountRepositoryMock.Setup(repo => repo.GetChildAccounts(It.IsAny<vAccount>()))
                .Returns((vAccount account) => testData.ChildAccounts(account))
                .Verifiable();

            accountManager = new AccountManager(accountRepositoryMock.Object);
        }

        [TestMethod]
        [Description("Verified AccountManager.GetOverview() calls the appropriate repository calls")]
        public void GetOverview_RepositoryCalled()
        {
            accountManager.GetOverview();

            accountRepositoryMock.Verify(mock => mock.GetRootAccounts());
            accountRepositoryMock.Verify(mock => mock.GetChildAccounts(It.IsAny<vAccount>()));
        }

        [TestMethod]
        [Description("Verified that Root Level Ascoutns are AccountManager.GetOverview()")]
        public void GetOverview_RootsCreated()
        {
            var accounts = accountManager.GetOverview();

            Assert.IsTrue(accounts.Count > 0);
        }

        [TestMethod]
        [Description("Verified that Subaccounts are AccountManager.GetOverview()")]
        public void GetOverview_SubaccountsCreated()
        {
            var accounts = accountManager.GetOverview();

            foreach(var account in accounts)
            {
                Assert.IsTrue(account.Subaccounts.Count > 0);
            }
        }

        [TestMethod]
        [Description("Verified that Shred Out Accounts are AccountManager.GetOverview()")]
        public void GetOverview_ShredacountsCreated()
        {
            var accounts = accountManager.GetOverview();

            foreach (var account in accounts)
            {
                foreach(var subaccount in account.Subaccounts)
                {
                    Assert.IsTrue(subaccount.Subaccounts.Count > 0);
                }
            }
        }
    }
}
