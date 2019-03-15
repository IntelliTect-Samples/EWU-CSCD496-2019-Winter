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

        [TestMethod]
        public void CanReachUsersAddPage()
        {
            WebDriver.Navigate().GoToUrl(UsersPage.Path);

            var userPage = new UsersPage(WebDriver);

            userPage.AddUserLink.Click();

            Assert.IsTrue(WebDriver.Url.EndsWith(AddUsersPage.Slug));
        }

        [TestMethod]
        public void CanReachUsersEditPage()
        {
            var usersPage = CreateUser("one", "two");
            var editLink = usersPage.GetEditLink("one two");

            editLink.Click();

            var editPage = new EditUsersPage(WebDriver);

            Assert.IsTrue(WebDriver.Url.Contains(EditUsersPage.Slug));

        }


        [TestMethod]
        public void AddValidUser()
        {
            var usersPage = CreateUser("first", "last");
            Assert.IsTrue(WebDriver.Url.EndsWith(UsersPage.Slug));

            var userNameList = usersPage.UserNames;
            Assert.IsTrue(userNameList.Contains("first last"));
        }

        [TestMethod]
        public void DeleteUser()
        {
            var usersPage = CreateUser("never", "ever");

            WebDriver.Navigate().GoToUrl(UsersPage.Path);
            usersPage.GetDeleteLink("never ever").Click();
            WebDriver.SwitchTo().Alert().Accept();
            var userNames = usersPage.UserNames;

            Assert.IsFalse(userNames.Contains("never ever"));

        }

        [TestCleanup]
        public void TestCleanup()
        {
            WebDriver.Quit();
            WebDriver.Dispose();
        }

        private UsersPage CreateUser(string firstName, string lastName)
        {
            WebDriver.Navigate().GoToUrl(AddUsersPage.Path);
            var usersPage = new UsersPage(WebDriver);
            var addUsersPage = new AddUsersPage(WebDriver);

            addUsersPage.FirstNameTextBox.SendKeys(firstName);
            addUsersPage.LastNameTextBox.SendKeys(lastName);
            addUsersPage.SubmitButton.Click();
            return usersPage;
        }
    }
}
