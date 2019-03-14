using System;
using System.Linq;
using OpenQA.Selenium;

namespace SecretSanta.Web.UITests
{
    public class AddUsersPage
    {
        public const string Slug = UsersPage.Slug + "/Add";
        public IWebDriver Driver { get; }
        public IWebElement HomeLink => Driver.FindElement(By.CssSelector(".navbar-brand>a"));
        public IWebElement UsersLink => Driver.FindElement(By.LinkText("Users"));
        public IWebElement UserFirstNameTextBox => Driver.FindElement(By.Id("FirstName"));
        public IWebElement UserLastNameTextBox => Driver.FindElement(By.Id("LastName"));
        public IWebElement SubmitButton =>
            Driver
                .FindElements(By.CssSelector("button.is-primary"))
                .Single(x => x.Text == "Submit");

        public IWebElement CancelLink => Driver.FindElement(By.LinkText("Cancel"));

        public AddUsersPage(IWebDriver driver)
        {
            Driver = driver ?? throw new ArgumentNullException(nameof(driver));
        }
    }
}