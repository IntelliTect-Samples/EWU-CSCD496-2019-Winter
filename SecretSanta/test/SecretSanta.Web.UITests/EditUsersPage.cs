using System;
using System.Linq;
using OpenQA.Selenium;

namespace SecretSanta.Web.UITests
{
    public class EditUsersPage
    {
        public IWebDriver Driver { get; }
        public IWebElement HomeLink => Driver.FindElement(By.CssSelector(".navbar-brand>a"));
        public IWebElement UsersLink => Driver.FindElement(By.LinkText("Users"));
        public IWebElement FirstNameTextBox => Driver.FindElement(By.Id("FirstName"));
        public IWebElement LastNameTextBox => Driver.FindElement(By.Id("LastName"));
        public string CurrentUserID =>
            Driver.Url.Substring(Driver.Url.LastIndexOf("/") + 1);

        public IWebElement SubmitButton =>
            Driver
                .FindElements(By.CssSelector("button.is-primary"))
                .Single(x => x.Text == "Submit");

        public IWebElement CancelLink => Driver.FindElement(By.LinkText("Cancel"));

        public EditUsersPage(IWebDriver driver)
        {
            Driver = driver ?? throw new ArgumentNullException(nameof(driver));
        }
    }
}