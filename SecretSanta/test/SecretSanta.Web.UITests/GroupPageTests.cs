using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SecretSanta.Web.UITests.Pages;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Web.UITests
{
    [TestClass]
    public class GroupPageTests
    {
        private IWebDriver WebDriver { get; set; }


        [TestInitialize]
        public void TestInit()
        {
            WebDriver = new ChromeDriver(".");
        }

        [TestMethod]
        public void CanReachGroupsPage()
        {
            WebDriver.Navigate().GoToUrl(HomePage.Path);

            var homePage = new HomePage(WebDriver);

            homePage.GroupsLink.Click();

            Assert.IsTrue(WebDriver.Url.EndsWith(GroupsPage.Slug));
        }

        [TestCleanup]
        public void TestCleanup()
        {
            WebDriver.Quit();
            WebDriver.Dispose();
        }
    }
}
