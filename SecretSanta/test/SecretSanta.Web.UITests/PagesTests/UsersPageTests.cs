using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SecretSanta.Web.UITests.Pages.AddPages;
using SecretSanta.Web.UITests.Pages.RootPages;
using System;
using System.IO;

namespace SecretSanta.Web.UITests
{
    [TestClass]
    public class UsersPageTests
    {
        private StructUser StructTestUser { get; set; }
        public const string RootUrl = "http://localhost:51042/";

        public IWebDriver Driver { get; set; }
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void Init()
        {
            Driver = new ChromeDriver(Path.GetFullPath("."));
            StructTestUser = new StructUser(
                                            "User_FirstName_" + Guid.NewGuid().ToString("N"),
                                            "User_LastName_" + Guid.NewGuid().ToString("N")
                                            );
        }

        [TestMethod]
        public void CanGetToUsersPage()
        {
            //Arrange
            Driver.Navigate().GoToUrl(RootUrl);

            //Act
            HomePage homePage = new HomePage(Driver);
            homePage.UsersLink.Click();

            //Assert
            Assert.IsTrue(Driver.Url.EndsWith(UsersPage.Slug));
        }

        [TestMethod]
        public void CanNavigateToAddUsersPage()
        {
            //Arrange
            UsersPage page = PrepNavigateToAddUsersPage();

            //Act
            page.AddUser.Click();

            //Assert
            Assert.IsTrue(Driver.Url.EndsWith(UsersPageAdd.Slug));
        }

        [TestMethod]
        public void CanAddUsers()
        {
            //Arrange /Act
            UsersPage page = CreateUser(StructTestUser.FirstName, StructTestUser.LastName);

            //Assert
            Assert.IsTrue(Driver.Url.EndsWith(UsersPage.Slug));

            string[] userNames = page.UserNames[page.UserNames.Count - 1].Split(" ");

            Assert.AreEqual(StructTestUser.FirstName, userNames[0]);
            Assert.AreEqual(StructTestUser.LastName, userNames[1]);

        }

        [TestMethod]
        public void CanEditUser()
        {
            //Arrange /Act
            UsersPage page = CreateUser(StructTestUser.FirstName, StructTestUser.LastName);

            //edit

            IWebElement editLink = page.GetEditLink(StructTestUser.FirstName + " " + StructTestUser.LastName);
            editLink.Click();
            EditUser(StructTestUser.LastName, StructTestUser.FirstName);

            //Assert
            Assert.IsTrue(Driver.Url.EndsWith(UsersPage.Slug));

            string[] userNames = page.UserNames[page.UserNames.Count - 1].Split(" ");

            Assert.AreNotEqual(StructTestUser.FirstName, userNames[0]);
            Assert.AreNotEqual(StructTestUser.LastName, userNames[1]);

            Assert.AreEqual(StructTestUser.LastName, userNames[0]);
            Assert.AreEqual(StructTestUser.FirstName, userNames[1]);
        }

        [TestMethod]
        public void CanDeleteUser()
        {
            //Arrange
            UsersPage page = CreateUser(StructTestUser.FirstName, StructTestUser.LastName);

            //Act
            IWebElement deleteLink = page.GetDeleteLink(StructTestUser.FirstName + " " + StructTestUser.LastName);
            deleteLink.Click();

            Driver.SwitchTo().Alert().Accept();

            //Assert
            if (page.UserNames.Count != 0)
            {
                string[] userNames = page.UserNames[page.UserNames.Count - 1].Split(" ");

                Assert.AreNotEqual(StructTestUser.FirstName, userNames[0]);
                Assert.AreNotEqual(StructTestUser.LastName, userNames[1]);
            }
        }

        private UsersPage CreateUser(string userFirstName, string userLastName)
        {
            UsersPage page = PrepNavigateToAddUsersPage();
            page.AddUser.Click();

            UsersPageAdd addUserPage = new UsersPageAdd(Driver);

            addUserPage.UserFirstNameTextBox.SendKeys(userFirstName);
            addUserPage.UserLastNameTextBox.SendKeys(userLastName);
            addUserPage.SubmitButton.Click();
            return page;
        }

        private void EditUser(string userFirstName, string userLastName)
        {
            UsersPageAdd addUserPage = new UsersPageAdd(Driver);
            addUserPage.UserFirstNameTextBox.Clear();
            addUserPage.UserFirstNameTextBox.SendKeys(userFirstName);
            addUserPage.UserLastNameTextBox.Clear();
            addUserPage.UserLastNameTextBox.SendKeys(userLastName);
            addUserPage.SubmitButton.Click();
        }

        private struct StructUser
        {
            public readonly string FirstName, LastName;

            public StructUser(string firstName, string lastName)
            {
                FirstName = firstName;
                LastName = lastName;
            }
        }

        private UsersPage PrepNavigateToAddUsersPage()
        {
            Uri rootUri = new Uri(RootUrl);
            Driver.Navigate().GoToUrl(new Uri(rootUri, UsersPage.Slug));
            return new UsersPage(Driver);
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (TestContext.CurrentTestOutcome == UnitTestOutcome.Failed)
            {
                string projectRoot = Environment.CurrentDirectory.Substring(0, Environment.CurrentDirectory.LastIndexOf("bin"));

                string fileName = projectRoot + "/Screenshots/" + TestContext.TestName + ".png";

                Screenshot screenshot = ((ITakesScreenshot)Driver).GetScreenshot();
                screenshot.SaveAsFile(fileName, ScreenshotImageFormat.Png);
            }

            Driver.Quit();
            Driver.Dispose();
        }
    }
}
