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

        private UsersPage CreateGroup(string groupName)
        {
            var rootUri = new Uri(RootUrl);
            Driver.Navigate().GoToUrl(new Uri(rootUri, GroupsPage.Slug));
            var page = new UsersPage(Driver);
            page.AddUser.Click();

            var addGroupPage = new AddGroupsPage(Driver);

            addGroupPage.GroupNameTextBox.SendKeys(groupName);
            addGroupPage.SubmitButton.Click();
            return page;
        }
    }

    public class UsersPage
    {
        public const string Slug = "Users";

        public IWebDriver Driver { get; }

        public IWebElement AddUser => Driver.FindElement(By.LinkText("Add User"));

        public AddUsersPage AddUserPage => new AddUsersPage(Driver);

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

        public IWebElement GetDeleteLink(string groupName)
        {
            ReadOnlyCollection<IWebElement> deleteLinks =
                Driver.FindElements(By.CssSelector("a.is-danger"));

            return deleteLinks.Single(x => x.GetAttribute("onclick").EndsWith($"{groupName}')"));
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

        public IWebElement UserNameTextBox => Driver.FindElement(By.Id("Name"));

        public IWebElement SubmitButton =>
            Driver
                .FindElements(By.CssSelector("button.is-primary"))
                .Single(x => x.Text == "Submit");

        public AddUsersPage(IWebDriver driver)
        {
            Driver = driver ?? throw new ArgumentNullException(nameof(driver));
        }
    }
}
