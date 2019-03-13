using System;
using System.Collections.Generic;
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
        public void CanNavigateToUsersPage()
        {
            Driver.Navigate().GoToUrl(RootUrl);

            var homePage = new HomePage(Driver);

            homePage.UsersLink.Click();

            Assert.IsTrue(Driver.Url.EndsWith(UsersPage.Slug));
        }

        [TestMethod]
        public void CanNavigateToAddUsersPage()
        {
            var rootUri = new Uri(RootUrl);
            Driver.Navigate().GoToUrl(new Uri(rootUri, UsersPage.Slug));
            var page = new UsersPage(Driver);

            page.AddUser.Click();

            Assert.IsTrue(Driver.Url.EndsWith(AddUsersPage.Slug));
        }

        [TestMethod]
        public void CanAddUsers()
        {
            string userFirstName = "User Name" + Guid.NewGuid().ToString("N");
            string userLastName = "Last Name" + Guid.NewGuid().ToString("N");
            string fullName = userFirstName + " " + userLastName; // this is what we'll use to search with
            UsersPage page = CreateUser(userFirstName, userLastName);

            Assert.IsTrue(Driver.Url.EndsWith(UsersPage.Slug));
            List<string> userNames = page.UserNames;
            Assert.IsTrue(userNames.Contains(fullName));
        }

        [TestMethod]
        public void CanDeleteUser()
        {
            string userFirstName = "User Name" + Guid.NewGuid().ToString("N");
            string userLastName = "Last Name" + Guid.NewGuid().ToString("N");
            string fullName = userFirstName + " " + userLastName; // this is what we'll use to search with
            UsersPage page = CreateUser(userFirstName, userLastName);

            IWebElement deleteLink = page.GetDeleteLink(fullName);
            deleteLink.Click();
            Driver.SwitchTo().Alert().Accept();

            List<string> userNames = page.UserNames;
            Assert.IsFalse(userNames.Contains(fullName));
        }

        [TestMethod]
        public void CanEditUser()
        {
            // Old user name
            string oldUserFirstName = "First Name" + Guid.NewGuid().ToString("N");
            string oldUserLastName = "Last Name" + Guid.NewGuid().ToString("N");
            string oldFullName = oldUserFirstName + " " + oldUserLastName; // this is what we'll use to search with

            // New user name
            string newUserFirstName = "First Name" + Guid.NewGuid().ToString("N");
            string newUserLastName = "Last Name" + Guid.NewGuid().ToString("N");
            string newFullName = newUserFirstName + " " + newUserLastName;

            UsersPage page = CreateUser(oldUserFirstName, oldUserLastName);

            // Make sure user was successfully added
            Assert.IsTrue(Driver.Url.EndsWith(UsersPage.Slug));
            List<string> userNames = page.UserNames;
            Assert.IsTrue(userNames.Contains(oldFullName));

            // Attempt to edit user
            IWebElement editLink = page.GetEditLink(oldFullName);
            editLink.Click();
            EditUsersPage editUsersPage = new EditUsersPage(Driver);
            editUsersPage.UserFirstNameTextBox.Clear();
            editUsersPage.UserFirstNameTextBox.SendKeys(newUserFirstName);
            editUsersPage.UserLastNameTextBox.Clear();
            editUsersPage.UserLastNameTextBox.SendKeys(newUserLastName);
            editUsersPage.SubmitButton.Click();

            // Ensure old username is removed and new user name exists
            userNames = page.UserNames;
            Assert.IsFalse(userNames.Contains(oldFullName));
            Assert.IsTrue(userNames.Contains(newFullName));
        }

        private UsersPage CreateUser(string firstName, string lastName)
        {
            var rootUri = new Uri(RootUrl);
            Driver.Navigate().GoToUrl(new Uri(rootUri, UsersPage.Slug));
            var page = new UsersPage(Driver);
            page.AddUser.Click();

            var addUserPage = new AddUsersPage(Driver);

            addUserPage.UserFirstNameTextBox.SendKeys(firstName);
            addUserPage.UserLastNameTextBox.SendKeys(lastName);
            addUserPage.SubmitButton.Click();
            return page;
        }
    }
}