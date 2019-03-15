using OpenQA.Selenium;
using System;

namespace SecretSanta.Web.UITests.Pages.RootPages
{
    public class HomePage
    {
        public IWebDriver Driver { get; }

        public GroupsPage GroupPage => new GroupsPage(Driver);

        //Id, LinkText, CssSelector/XPath
        //public IWebElement GroupsLink => Driver.FindElement(By.CssSelector("a[href=\"/Groups\"]"));
        public IWebElement GroupsLink => Driver.FindElement(By.LinkText("Groups"));
        public IWebElement UsersLink => Driver.FindElement(By.LinkText("Users"));
        public IWebElement GiftsLink => Driver.FindElement(By.LinkText("Gifts"));

        public HomePage(IWebDriver driver)
        {
            Driver = driver ?? throw new ArgumentNullException(nameof(driver));
        }
    }
}
