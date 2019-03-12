using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;

namespace SecretSanta.Web.UITests
{
    [TestClass]
    public class UserPageTests
    {
        private const string RootUrl = "https://localhost:44308/";
        private IWebDriver Driver { get; set; }

        [TestInitialize]
        public void Init()
        {
            Driver = new ChromeDriver(Path.GetFullPath("."));
        }

        [TestCleanup]
        public void Cleanup()
        {
            //Driver.Quit();
            //Driver.Dispose();
        }

        [TestMethod]
        public void CanAddUser()
        {
            Driver.Navigate().GoToUrl(RootUrl);

            var homePage = new HomePage(Driver);
        }
    }
}
