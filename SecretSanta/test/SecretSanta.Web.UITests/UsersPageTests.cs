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
    public class UsersPageTests
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
        public void CanNavigate_FromUsersPage_ToHomePage()
        {
            // Arrange
            Driver.Navigate().GoToUrl($"{RootUrl}{UsersPage.Slug}");

            // Act
            UsersPage usersPage = new UsersPage(Driver);
            usersPage.HomeLink.Click();

            // Assert
            Assert.AreEqual<string>(RootUrl, Driver.Url);
        }

        [TestMethod]
        public void CanNavigate_FromUsersPage_ToAddUsersPage()
        {
            // Arrange
            Driver.Navigate().GoToUrl($"{RootUrl}{UsersPage.Slug}");

            // Act
            UsersPage usersPage = new UsersPage(Driver);
            usersPage.AddUserLink.Click();

            // Assert
            Assert.AreEqual<string>(RootUrl + AddUsersPage.Slug, Driver.Url);
        }

        [TestMethod] // TEST FAILURE: UI doesn't allow navigation from add page to home page
        public void CanNavigate_FromAddUsersPage_ToHomePage()
        {
            // Arrange
            Driver.Navigate().GoToUrl($"{RootUrl}{AddUsersPage.Slug}");

            // Act
            AddUsersPage addUsersPage = new AddUsersPage(Driver);
            addUsersPage.HomeLink.Click();

            // Assert
            Assert.AreEqual<string>(RootUrl, Driver.Url);
        }

        [TestMethod]
        public void CanNavigate_FromAddUsersPage_ToUsersPage()
        {
            // Arrange
            Driver.Navigate().GoToUrl($"{RootUrl}{AddUsersPage.Slug}");

            // Act
            AddUsersPage addUsersPage = new AddUsersPage(Driver);
            addUsersPage.UsersLink.Click();

            // Assert
            Assert.IsTrue(Driver.Url.EndsWith(UsersPage.Slug));
        }

        [TestMethod]
        public void CanAddUser()
        {
            // Arrange
            string userFirstName = "First Name" + Guid.NewGuid().ToString("N");
            string userLastName = "Last Name" + Guid.NewGuid().ToString("N");

            // Act
            UsersPage page = CreateUser(userFirstName, userLastName);

            // Assert
            Assert.IsTrue(Driver.Url.EndsWith(UsersPage.Slug));
            List<string> userNames = page.UserNames;
            Assert.IsTrue(userNames.Contains($"{userFirstName} {userLastName}"));
        }

        [TestMethod]
        public void CanAddUser_NoLastName()
        {
            // Arrange
            string userFirstName = "First Name" + Guid.NewGuid().ToString("N");
            string userLastName = "";

            // Act
            UsersPage page = CreateUser(userFirstName, userLastName);

            // Assert
            Assert.IsTrue(Driver.Url.EndsWith(UsersPage.Slug));
            List<string> userNames = page.UserNames;
            Assert.IsTrue(userNames.Contains($"{userFirstName}"));
        }

        [TestMethod]
        public void CanCancelAddUser()
        {
            // Arrange
            Driver.Navigate().GoToUrl($"{RootUrl}{UsersPage.Slug}");
            string userFirstName = "First Name" + Guid.NewGuid().ToString("N");
            string userLastName = "Last Name" + Guid.NewGuid().ToString("N");

            UsersPage usersPage = new UsersPage(Driver);
            usersPage.AddUserLink.Click();

            AddUsersPage addUsersPage = new AddUsersPage(Driver);
            addUsersPage.UserFirstNameTextBox.SendKeys(userFirstName);
            addUsersPage.UserLastNameTextBox.SendKeys(userLastName);

            // Act
            addUsersPage.CancelLink.Click();

            // Assert
            Assert.IsTrue(Driver.Url.EndsWith(UsersPage.Slug));
            List<string> userNames = usersPage.UserNames;
            Assert.IsFalse(userNames.Contains($"{userFirstName} {userLastName}"));
        }

        [TestMethod]
        public void CanNavigate_FromUsersPage_ToEditUsersPage()
        {
            // Arrange
            string userFirstName = "First Name" + Guid.NewGuid().ToString("N");
            string userLastName = "Last Name" + Guid.NewGuid().ToString("N");
            UsersPage usersPage = CreateUser(userFirstName, userLastName);

            // Act
            IWebElement editLink = usersPage.GetEditLink($"{userFirstName} {userLastName}");
            string linkText = editLink.GetAttribute("href");
            string userID = (linkText.Substring(linkText.LastIndexOf("/") + 1));
            EditUsersPage editUsersPage = new EditUsersPage(Driver);
            editLink.Click();

            // Assert
            Assert.AreEqual<string>(userID, editUsersPage.CurrentUserID);
            Assert.AreEqual<string>(userFirstName, editUsersPage.FirstNameTextBox.GetAttribute("value"));
            Assert.AreEqual<string>(userLastName, editUsersPage.LastNameTextBox.GetAttribute("value"));
        }

        [TestMethod] // TEST FAILURE: UI doesn't allow navigation from edit page to home page
        public void CanNavigate_FromEditUsersPage_ToHomePage()
        {
            // Arrange
            string userFirstName = "First Name" + Guid.NewGuid().ToString("N");
            string userLastName = "Last Name" + Guid.NewGuid().ToString("N");
            UsersPage usersPage = CreateUser(userFirstName, userLastName);
            usersPage.GetEditLink($"{userFirstName} {userLastName}").Click();

            // Act
            EditUsersPage editUsersPage = new EditUsersPage(Driver);
            editUsersPage.HomeLink.Click();

            // Assert
            Assert.AreEqual<string>(RootUrl, Driver.Url);
        }

        [TestMethod]
        public void CanNavigate_FromEditUsersPage_ToUsersPage()
        {
            // Arrange
            string userFirstName = "First Name" + Guid.NewGuid().ToString("N");
            string userLastName = "Last Name" + Guid.NewGuid().ToString("N");
            UsersPage usersPage = CreateUser(userFirstName, userLastName);
            usersPage.GetEditLink($"{userFirstName} {userLastName}").Click();

            // Act
            EditUsersPage editUsersPage = new EditUsersPage(Driver);
            editUsersPage.UsersLink.Click();

            // Assert
            Assert.IsTrue(Driver.Url.EndsWith(UsersPage.Slug));
        }

        [TestMethod]
        public void CanEditUser()
        {
            // Arrange
            string userFirstName = "First Name" + Guid.NewGuid().ToString("N");
            string userLastName = "Last Name" + Guid.NewGuid().ToString("N");
            UsersPage usersPage = CreateUser(userFirstName, userLastName);
            usersPage.GetEditLink($"{userFirstName} {userLastName}").Click();
            EditUsersPage editUsersPage = new EditUsersPage(Driver);

            // Act
            editUsersPage.FirstNameTextBox.Clear();
            editUsersPage.LastNameTextBox.Clear();

            string newFirstName = "First Name" + Guid.NewGuid().ToString("N");
            string newLastName = "Last Name" + Guid.NewGuid().ToString("N");

            editUsersPage.FirstNameTextBox.SendKeys(newFirstName);
            editUsersPage.LastNameTextBox.SendKeys(newLastName);
            editUsersPage.SubmitButton.Click();

            // Assert
            List<string> userNames = usersPage.UserNames;
            Assert.IsTrue(userNames.Contains($"{newFirstName} {newLastName}"));
            Assert.IsFalse(userNames.Contains($"{userFirstName} {userLastName}"));
        }

        [TestMethod]
        public void CanEditUser_NoLastName()
        {
            // Arrange
            string userFirstName = "First Name" + Guid.NewGuid().ToString("N");
            string userLastName = "Last Name" + Guid.NewGuid().ToString("N");
            UsersPage usersPage = CreateUser(userFirstName, userLastName);
            usersPage.GetEditLink($"{userFirstName} {userLastName}").Click();
            EditUsersPage editUsersPage = new EditUsersPage(Driver);

            // Act
            editUsersPage.FirstNameTextBox.Clear();
            editUsersPage.LastNameTextBox.Clear();

            string newFirstName = "First Name" + Guid.NewGuid().ToString("N");
            string newLastName = "";

            editUsersPage.FirstNameTextBox.SendKeys(newFirstName);
            editUsersPage.LastNameTextBox.SendKeys(newLastName);
            editUsersPage.SubmitButton.Click();

            // Assert
            List<string> userNames = usersPage.UserNames;
            Assert.IsTrue(userNames.Contains($"{newFirstName}"));
            Assert.IsFalse(userNames.Contains($"{userFirstName} {userLastName}"));
        }

        [TestMethod]
        public void CanCancelEditUser()
        {
            // Arrange
            string userFirstName = "First Name" + Guid.NewGuid().ToString("N");
            string userLastName = "Last Name" + Guid.NewGuid().ToString("N");
            UsersPage usersPage = CreateUser(userFirstName, userLastName);
            usersPage.GetEditLink($"{userFirstName} {userLastName}").Click();
            EditUsersPage editUsersPage = new EditUsersPage(Driver);

            // Act
            editUsersPage.CancelLink.Click();

            // Assert
            Assert.IsTrue(Driver.Url.EndsWith(UsersPage.Slug));
            List<string> userNames = usersPage.UserNames;
            Assert.IsTrue(userNames.Contains($"{userFirstName} {userLastName}"));
        }

        [TestMethod]
        public void CanDeleteUser()
        {
            // Arrange
            string userFirstName = "First Name " + Guid.NewGuid().ToString("N");
            string userLastName = "Last Name " + Guid.NewGuid().ToString("N");
            UsersPage usersPage = CreateUser(userFirstName, userLastName);

            // Act
            IWebElement deleteLink = usersPage.GetDeleteLink(userFirstName + " " + userLastName);
            deleteLink.Click();
            Driver.SwitchTo().Alert().Accept();
            
            // Assert
            List<string> userNames = usersPage.UserNames;
            Assert.IsFalse(userNames.Contains($"{userFirstName} {userLastName}"));
        }

        [TestMethod]
        public void CanCancelDeleteUser()
        {
            // Arrange
            string userFirstName = "First Name " + Guid.NewGuid().ToString("N");
            string userLastName = "Last Name " + Guid.NewGuid().ToString("N");
            UsersPage usersPage = CreateUser(userFirstName, userLastName);

            // Act
            IWebElement deleteLink = usersPage.GetDeleteLink(userFirstName + " " + userLastName);
            deleteLink.Click();
            Driver.SwitchTo().Alert().Dismiss();

            // Assert
            Assert.IsTrue(Driver.Url.EndsWith(UsersPage.Slug));
            List<string> userNames = usersPage.UserNames;
            Assert.IsTrue(userNames.Contains($"{userFirstName} {userLastName}"));
        }

        private UsersPage CreateUser(string userFirstName, string userLastName)
        {
            Uri rootUri = new Uri(RootUrl);
            Driver.Navigate().GoToUrl(new Uri(rootUri, UsersPage.Slug));

            UsersPage page = new UsersPage(Driver);
            page.AddUserLink.Click();

            AddUsersPage addUserPage = new AddUsersPage(Driver);

            addUserPage.UserFirstNameTextBox.SendKeys(userFirstName);
            addUserPage.UserLastNameTextBox.SendKeys(userLastName);
            addUserPage.SubmitButton.Click();
            return page;
        }
    }
}
