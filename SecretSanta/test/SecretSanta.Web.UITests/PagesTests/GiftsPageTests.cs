using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SecretSanta.Web.UITests.Pages.RootPages;
using System;
using System.IO;

namespace SecretSanta.Web.UITests.PagesTests
{
    [TestClass]
    public class GiftsPageTests : AbstractParentTest
    {

        [TestMethod]
        public void CanGetToGiftPage()
        {
            //Arrange
            Driver.Navigate().GoToUrl(RootUrl);

            //Act
            HomePage homePage = new HomePage(Driver);
            homePage.GiftsLink.Click();

            //Assert
            Assert.IsTrue(Driver.Url.EndsWith(GiftsPage.Slug));
        }
    }
}
