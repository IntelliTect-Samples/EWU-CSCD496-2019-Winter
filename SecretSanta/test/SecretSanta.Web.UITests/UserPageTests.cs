using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using SecretSanta.Web.UITests.Pages;

namespace SecretSanta.Web.UITests
{
    [TestClass]
    public class UserPageTests
    {
        private const string RootUrl = "https://localhost:44377/";

        private IWebDriver Driver { get; set; }

        [TestInitialize]
        public void Init()
        {
            var options = new FirefoxOptions
            {
                AcceptInsecureCertificates = true
            };
            Driver = new FirefoxDriver(Path.GetFullPath("."), options);
        }

        [TestCleanup]
        public void Cleanup()
        {
            Driver.Quit();
            Driver.Dispose();
        }

        [TestMethod]
        public void CanGetToUsersPage()
        {
            //Arrange
            Driver.Navigate().GoToUrl(RootUrl);

            //Act
            var homePage = new HomePage(Driver);
            homePage.UsersLink.Click();

            //Assert
            Assert.IsTrue(Driver.Url.EndsWith(UsersPage.Slug));
        }

        [TestMethod]
        public void CanNavigateToAddUsersPage()
        {
            //Arrange
            var rootUri = new Uri(RootUrl);
            Driver.Navigate().GoToUrl(new Uri(rootUri, UsersPage.Slug));
            var page = new UsersPage(Driver);

            //Act
            page.AddUser.Click();

            //Assert
            Assert.IsTrue(Driver.Url.EndsWith(AddUsersPage.Slug));
        }

        [TestMethod]
        public void CanAddUsers()
        {
            //Arrange /Act
            string userFirstName = "First Name" + Guid.NewGuid().ToString("N");
            string userLastName = "Last Name" + Guid.NewGuid().ToString("N");
            string userFullName = userFirstName + " " + userLastName;
            UsersPage page = CreateUser(userFirstName, userLastName);

            //Assert
            Assert.IsTrue(Driver.Url.EndsWith(UsersPage.Slug));
            List<string> userNames = page.UserNames;
            Assert.IsTrue(userNames.Contains(userFullName));
        }

        [TestMethod]
        public void CanGetErrorOnAddUserMissingFirstName()
        {
            string userLastName = "Last Name" + Guid.NewGuid().ToString("N");

            UsersPage page = CreateUser(null, userLastName);

            Assert.IsTrue(Driver.Url.EndsWith(AddUsersPage.Slug));

            AddUsersPage addPage = new AddUsersPage(Driver);

            Assert.IsNotNull(addPage.Driver.FindElements(By.CssSelector("ul>li")).Single(x => x.Text == "The FirstName field is required."));
        }

        [TestMethod]
        public void CanCancelAddUsers()
        {
            //Arrange
            var rootUri = new Uri(RootUrl);
            Driver.Navigate().GoToUrl(new Uri(rootUri, UsersPage.Slug));
            var page = new UsersPage(Driver);

            //Act
            page.AddUser.Click();

            //Assert that we are at the right place
            Assert.IsTrue(Driver.Url.EndsWith(AddUsersPage.Slug));

            //Setup Add Users Page
            AddUsersPage addPage = new AddUsersPage(Driver);

            //Act
            addPage.CancelButton.Click();

            //Assert that we are now back at the users page
            Assert.IsTrue(Driver.Url.EndsWith(UsersPage.Slug));
        }

        [TestMethod]
        public void CanDeleteUser()
        {
            //Arrange
            string userFirstName = "First Name" + Guid.NewGuid().ToString("N");
            string userLastName = "Last Name" + Guid.NewGuid().ToString("N");
            string userFullName = userFirstName + " " + userLastName;
            UsersPage page = CreateUser(userFirstName, userLastName);

            //Act
            IWebElement deleteLink = page.GetDeleteLink(userFullName);
            deleteLink.Click();

            Driver.SwitchTo().Alert().Accept();

            //Assert
            List<string> userNames = page.UserNames;
            Assert.IsFalse(userNames.Contains(userFullName));
        }

        [TestMethod]
        public void CanCancelDeleteUser()
        {
            //Arrange
            string userFirstName = "First Name" + Guid.NewGuid().ToString("N");
            string userLastName = "Last Name" + Guid.NewGuid().ToString("N");
            string userFullName = userFirstName + " " + userLastName;
            UsersPage page = CreateUser(userFirstName, userLastName);

            //Act
            IWebElement deleteLink = page.GetDeleteLink(userFullName);
            deleteLink.Click();

            Driver.SwitchTo().Alert().Dismiss();

            //Assert
            List<string> userNames = page.UserNames;
            Assert.IsTrue(userNames.Contains(userFullName));
        }

        [TestMethod]
        public void CanNavigateToEditUsersPage()
        {
            //Arrange
            var rootUri = new Uri(RootUrl);
            Driver.Navigate().GoToUrl(new Uri(rootUri, UsersPage.Slug));
            var page = new UsersPage(Driver);

            //Act
            page.EditUser.Click();

            //Assert
            Assert.IsTrue(Driver.Url.Contains(EditUsersPage.Slug));
        }

        [TestMethod]
        public void CanEditUser()
        {
            //Arrange
            string userFirstName = "First Name" + Guid.NewGuid().ToString("N");
            string userLastName = "Last Name" + Guid.NewGuid().ToString("N");
            string userFullName = userFirstName + " " + userLastName;

            string userNewFirstName = "First Name" + Guid.NewGuid().ToString("N");
            string userNewLastName = "Last Name" + Guid.NewGuid().ToString("N");
            string userNewFullName = userNewFirstName + " " + userNewLastName;

            UsersPage page = CreateUser(userFirstName, userLastName);

            page.GetEditLink(userFullName).Click();
            EditUsersPage editPage = new EditUsersPage(Driver);
            editPage.UserFristNameTextBox.Clear();
            editPage.UserLastNameTextBox.Clear();

            editPage.UserFristNameTextBox.SendKeys(userNewFirstName);
            editPage.UserLastNameTextBox.SendKeys(userNewLastName);
            editPage.SubmitButton.Click();

            List<string> userNames = page.UserNames;
            Assert.IsTrue(userNames.Contains(userNewFullName));
            Assert.IsFalse(userNames.Contains(userFullName));
        }

        [TestMethod]
        public void CanCancelEditUsers()
        {
            //Arrange
            string userFirstName = "First Name" + Guid.NewGuid().ToString("N");
            string userLastName = "Last Name" + Guid.NewGuid().ToString("N");
            string userFullName = userFirstName + " " + userLastName;

            UsersPage page = CreateUser(userFirstName, userLastName);

            page.GetEditLink(userFullName).Click();
            EditUsersPage editPage = new EditUsersPage(Driver);

            editPage.CancelButton.Click();

            Assert.IsTrue(Driver.Url.EndsWith(UsersPage.Slug));
        }

        private UsersPage CreateUser(string userFirstName, string userLastName)
        {
            var rootUri = new Uri(RootUrl);
            Driver.Navigate().GoToUrl(new Uri(rootUri, UsersPage.Slug));
            var page = new UsersPage(Driver);
            page.AddUser.Click();

            var addUsersPage = new AddUsersPage(Driver);

            if(userFirstName != null) addUsersPage.UserFristNameTextBox.SendKeys(userFirstName);
            addUsersPage.UserLastNameTextBox.SendKeys(userLastName);
            addUsersPage.SubmitButton.Click();
            return page;
        }
    }
}
