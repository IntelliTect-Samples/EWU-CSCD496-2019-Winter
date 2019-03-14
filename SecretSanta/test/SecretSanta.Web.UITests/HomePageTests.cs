using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SecretSanta.Web.UITests
{
    [TestClass]
    public class HomePageTests
    {
        private const string RootUrl = "https://localhost:44398/";

        private IWebDriver Driver { get; set; }

        [TestInitialize]
        public void Init()
        {
            ChromeOptions options = new ChromeOptions
            {
                BinaryLocation = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe"
            };
            Driver = new ChromeDriver(Path.GetFullPath("."), options);
        }

        [TestCleanup]
        public void Cleanup()
        {
            Driver.Quit();
            Driver.Dispose();
        }

        [TestMethod]
        public void CanNavigateToUsersPage()
        {
            // Arrange
            Driver.Navigate().GoToUrl(RootUrl);

            // Act
            HomePage homePage = new HomePage(Driver);
            homePage.UsersLink.Click();

            // Assert
            Assert.IsTrue(Driver.Url.EndsWith(UsersPage.Slug));
        }
    }
}
