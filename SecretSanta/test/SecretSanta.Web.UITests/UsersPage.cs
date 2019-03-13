using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using OpenQA.Selenium;

namespace SecretSanta.Web.UITests
{
    class UsersPage
    {
        public IWebDriver Driver { get; set; }
        public const string Slug = "Users";
        public IWebElement AddUser => Driver.FindElement(By.LinkText("Add User"));
        public IWebElement EditUser => Driver.FindElement(By.LinkText("Edit"));
        public IWebElement DeleteUser => Driver.FindElement(By.LinkText("Delete"));
        public AddUsersPage AddUsersPage => new AddUsersPage(Driver);

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

        public IWebElement GetEditLink(string userName)
        {
            ReadOnlyCollection<IWebElement> editLinks =
                Driver.FindElements(By.CssSelector("h1+ul>li"));

            return editLinks.Single(x => x.Text == (userName + " Edit Delete")).FindElement(By.LinkText("Edit"));
        }

        public IWebElement GetDeleteLink(string userName)
        {
            ReadOnlyCollection<IWebElement> deleteLinks =
                Driver.FindElements(By.CssSelector("a.is-danger"));

            return deleteLinks.Single(x => x.GetAttribute("onclick").EndsWith($"{userName}?')"));
        }

        public UsersPage(IWebDriver driver)
        {
            Driver = driver ?? throw new ArgumentNullException(nameof(driver));
        }
    }
}
