using OpenQA.Selenium;
using SecretSanta.Web.UITests.Pages.RootPages;
using System;
using System.Linq;

namespace SecretSanta.Web.UITests.Pages.AddPages
{
    public class GroupsPageAdd
    {

        public const string Slug = GroupsPage.Slug + "/Add";

        public IWebDriver Driver { get; }

        public IWebElement GroupNameTextBox => Driver.FindElement(By.Id("Name"));

        public IWebElement SubmitButton =>
            Driver
                .FindElements(By.CssSelector("button.is-primary"))
                .Single(x => x.Text == "Submit");

        public GroupsPageAdd(IWebDriver driver)
        {
            Driver = driver ?? throw new ArgumentNullException(nameof(driver));
        }
    }
}
