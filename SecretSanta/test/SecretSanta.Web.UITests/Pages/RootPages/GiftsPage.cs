using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Web.UITests.Pages.RootPages
{
    public class GiftsPage
    {
        public const string Slug = "Gifts";

        public IWebDriver Driver { get; }

        public GroupsPage GroupPage => new GroupsPage(Driver);

        public IWebElement GroupsLink => Driver.FindElement(By.LinkText("Groups"));
        public IWebElement UsersLink => Driver.FindElement(By.LinkText("Users"));
        public IWebElement GiftsLink => Driver.FindElement(By.LinkText("Gifts"));

    }
}
