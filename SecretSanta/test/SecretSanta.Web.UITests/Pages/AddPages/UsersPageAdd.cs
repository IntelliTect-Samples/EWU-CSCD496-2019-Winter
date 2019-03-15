using OpenQA.Selenium;
using SecretSanta.Web.UITests.Pages.RootPages;
using System;
using System.Linq;

namespace SecretSanta.Web.UITests.Pages.AddPages
{
    public class UsersPageAdd
    {

        public const string Slug = UsersPage.Slug + "/Add";

        public IWebDriver Driver { get; }

        public IWebElement UserFirstNameTextBox => Driver.FindElement(By.Id("FirstName"));
        public IWebElement UserLastNameTextBox => Driver.FindElement(By.Id("LastName"));

        public IWebElement SubmitButton =>
            Driver
                .FindElements(By.CssSelector("button.is-primary"))
                .Single(x => x.Text == "Submit");

        public UsersPageAdd(IWebDriver driver)
        {
            Driver = driver ?? throw new ArgumentNullException(nameof(driver));
        }
    }
}
