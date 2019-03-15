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
    public class GiftPageTests
    {
        private IWebDriver WebDriver { get; set; }

        [TestInitialize]
        public void TestInit()
        {
            WebDriver = new ChromeDriver(".");
        }


        [TestMethod]
        public void CanReachGiftPage()
        {
            WebDriver.Navigate().GoToUrl(HomePage.Path);

            var homePage = new HomePage(WebDriver);

            homePage.GiftsLink.Click();

            Assert.IsTrue(WebDriver.Url.EndsWith(GiftPage.Slug));
        }

        [TestCleanup]
        public void TestCleanup()
        {
            WebDriver.Quit();
            WebDriver.Dispose();
        }
    }
}
