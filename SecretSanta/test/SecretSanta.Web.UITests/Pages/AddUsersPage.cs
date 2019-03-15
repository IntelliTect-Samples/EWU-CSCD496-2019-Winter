using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecretSanta.Web.UITests.Pages
{
    public class AddUsersPage
    {
        public const string Path = UsersPage.Path + "Add/";
        public const string Slug = UsersPage.Slug + "/Add";

        public IWebDriver WebDriver { get; set; }

        public AddUsersPage(IWebDriver webDriver)
        {
            WebDriver = webDriver ?? throw new ArgumentNullException(nameof(webDriver));
        }

        public IWebElement FirstNameTextBox => WebDriver.FindElement(By.Id("FirstName"));
        public IWebElement LastNameTextBox => WebDriver.FindElement(By.Id("LastName"));

        public IWebElement SubmitButton =>
            WebDriver
                .FindElements(By.CssSelector("button.is-primary"))
                .Single(x => x.Text == "Submit");

    }
}
