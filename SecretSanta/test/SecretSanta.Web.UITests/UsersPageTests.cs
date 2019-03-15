using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SecretSanta.Web.UITests.Pages;

namespace SecretSanta.Web.UITests
{
    [TestClass]
    public class UsersPageTests
    {
        private IWebDriver WebDriver { get; set; }

        [TestInitialize]
        public void TestInit()
        {
            WebDriver = new ChromeDriver(".");
        }

        [TestMethod]
        public void CanReachUsersPage()
        {
            WebDriver.Navigate().GoToUrl(HomePage.Path);

            var homePage = new HomePage(WebDriver);

            homePage.UsersLink.Click();

            Assert.IsTrue(WebDriver.Url.EndsWith(UsersPage.Slug));
        }


        [TestCleanup]
        public void TestCleanup()
        {
            WebDriver.Quit();
            WebDriver.Dispose();
        }
    }
}
