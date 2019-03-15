using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SecretSanta.Web.UITests.Pages.RootPages;
using System;
using System.IO;

namespace SecretSanta.Web.UITests
{
    [TestClass]
    public class GiftsPageTests
    {
        protected const string RootUrl = "http://localhost:51042/";

        protected IWebDriver Driver { get; set; }
        public TestContext TestContext { get; set; }


        [TestInitialize]
        public void Init()
        {
            Driver = new ChromeDriver(Path.GetFullPath("."));
        }

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

        [TestCleanup]
        public void Cleanup()
        {
            if (TestContext.CurrentTestOutcome == UnitTestOutcome.Failed)
            {
                string root = Environment.CurrentDirectory.Substring(0, Environment.CurrentDirectory.LastIndexOf("bin"));

                string fileName = $"{root}/TestErrorScreenshots/{TestContext.TestName}.png";

                Screenshot screenshot = ((ITakesScreenshot)Driver).GetScreenshot();
                screenshot.SaveAsFile(fileName, ScreenshotImageFormat.Png);
            }

            Driver.Quit();
            Driver.Dispose();
        }
    }
}
