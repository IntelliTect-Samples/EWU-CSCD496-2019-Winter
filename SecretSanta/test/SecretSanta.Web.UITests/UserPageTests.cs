using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace SecretSanta.Web.UITests
{
    [TestClass]
    public class UserPageTests
    {
        private const string RootUrl = "https://localhost:44387/";

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
        public void CanAddUsers()
        {
            //Arrange
            string firstName = "FirstName " + Guid.NewGuid().ToString("N");

            //Act
            UsersPage page = CreateUser(firstName);

            //Assert
            Assert.IsTrue(Driver.Url.EndsWith(UsersPage.Slug));
            List<string> userNames = page.UserNames;
            Assert.IsTrue(userNames.Contains(firstName));
        }

        [TestMethod]
        public void CanDeleteUser()
        {
            //Arrange
            string userName = Arrange();
            UsersPage page = CreateUser(userName);

            //Act
            IWebElement deleteLink = page.GetDeleteLink(userName);
            deleteLink.Click();

            Driver.SwitchTo().Alert().Accept();

            //Assert
            List<string> groupNames = page.UserNames;
            Assert.IsFalse(groupNames.Contains(userName));
        }

        [TestMethod]
        public void CanEditUser()
        {
            //Arrange
            var rootUri = new Uri(RootUrl);
            Driver.Navigate().GoToUrl(new Uri(rootUri, UsersPage.Slug));
            string userName = Arrange();
            var userPage = CreateUser(userName);

            IWebElement editLink = userPage.GetEditLink(userName);
            editLink.Click();

            //Act
            var editUserPage = new EditUsersPage(Driver);
            string editedName = "EDITED USER";
            editUserPage.FirstNameTextBox.SendKeys(editedName);
            editUserPage.SubmitButton.Click();

            //Assert
            List<string> userNames = userPage.UserNames;
            Assert.IsFalse(userNames.Contains(userName));
            Assert.IsTrue(userNames.Contains(userName + editedName));
        }

        private string Arrange()
        {
            return "User Name " + Guid.NewGuid().ToString("N");
        }

        private UsersPage CreateUser(string firstName)
        {
            var rootUri = new Uri(RootUrl);
            Driver.Navigate().GoToUrl(new Uri(rootUri, UsersPage.Slug));
            var page = new UsersPage(Driver);
            page.AddUser.Click();

            var addUserPage = new AddUsersPage(Driver);

            addUserPage.FirstNameTextBox.SendKeys(firstName);
            addUserPage.SubmitButton.Click();
            return page;
        }
    }

    public class UsersPage
    {
        public const string Slug = "Users";

        public IWebDriver Driver { get; }

        public IWebElement AddUser => Driver.FindElement(By.LinkText("Add User"));

        public AddUsersPage AddUserPage => new AddUsersPage(Driver);
        public EditUsersPage EditUserPage => new EditUsersPage(Driver);

        public List<string> UserNames
        {
            get
            {
                var elements = Driver.FindElements(By.CssSelector("h1+ul>li"));

                return elements
                    .Select(x =>
                    {
                        var text = x.Text;
                        if (text.EndsWith(" Edit Delete"))
                        {
                            text = text.Substring(0, text.Length - " Edit Delete".Length);
                        }
                        return text;
                    })
                    .ToList();
            }
        }

        public IWebElement GetDeleteLink(string userName)
        {
            ReadOnlyCollection<IWebElement> deleteLinks =
                Driver.FindElements(By.CssSelector("a.is-danger"));

            return deleteLinks.Single(x => x.GetAttribute("onclick").EndsWith($"{userName}?')"));
        }

        public IWebElement GetEditLink(string userName)
        {
            IWebElement li = null;
            ReadOnlyCollection<IWebElement> lis = Driver.FindElements(By.TagName("li"));
            foreach(IWebElement element in lis)
            {
                if(element.Text.Contains(userName))
                {
                    li = element.FindElement(By.CssSelector("a.is-warning"));
                }
            }
            return li;
        }

        public UsersPage(IWebDriver driver)
        {
            Driver = driver ?? throw new ArgumentNullException(nameof(driver));
        }
    }

    public class AddUsersPage
    {

        public const string Slug = UsersPage.Slug + "/Add";

        public IWebDriver Driver { get; }

        public IWebElement FirstNameTextBox => Driver.FindElement(By.Id("FirstName"));

        public IWebElement SubmitButton =>
            Driver
                .FindElements(By.CssSelector("button.is-primary"))
                .Single(x => x.Text == "Submit");

        public AddUsersPage(IWebDriver driver)
        {
            Driver = driver ?? throw new ArgumentNullException(nameof(driver));
        }
    }

    public class EditUsersPage
    {
        public const string Slug = UsersPage.Slug + "/Edit";

        public IWebDriver Driver { get; }

        public IWebElement FirstNameTextBox => Driver.FindElement(By.Id("FirstName"));

        public IWebElement SubmitButton =>
            Driver
                .FindElements(By.CssSelector("button.is-primary"))
                .Single(x => x.Text == "Submit");

        public EditUsersPage(IWebDriver driver)
        {
            Driver = driver ?? throw new ArgumentNullException(nameof(driver));
        }
    }
}
