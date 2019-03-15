using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SecretSanta.Web.UITests.Pages.AddPages;

namespace SecretSanta.Web.UITests.Pages.RootPages
{
    public class UsersPage
    {
        public const string Slug = "Users";

        public IWebDriver Driver { get; }

        public IWebElement AddUser => Driver.FindElement(By.LinkText("Add User"));

        public UsersPageAdd AddUsersPage => new UsersPageAdd(Driver);

        public List<string> UserNames
        {
            get
            {
                ReadOnlyCollection<IWebElement> elements = Driver.FindElements(By.CssSelector("h1+ul>li"));

                return elements
                    .Select(x =>
                    {
                        string text = x.Text;
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

            IWebElement deleteLink = deleteLinks.Single(x => x.GetAttribute("onclick").EndsWith($"{userName}?')"));
            return deleteLink;
        }

        public UsersPage(IWebDriver driver)
        {
            Driver = driver ?? throw new ArgumentNullException(nameof(driver));
        }

        internal IWebElement GetEditLink(string userName)
        {
            return Driver.FindElements(By.PartialLinkText("Edit")).Last();
        }
    }
}
