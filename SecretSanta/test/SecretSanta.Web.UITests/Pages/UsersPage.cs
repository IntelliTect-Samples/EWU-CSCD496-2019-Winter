using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using OpenQA.Selenium;

namespace SecretSanta.Web.UITests.Pages
{
    public class UsersPage
    {
        public const string Slug = "Users";

        public IWebDriver Driver { get; }

        public IWebElement AddUser => Driver.FindElement(By.LinkText("Add User"));

        public IWebElement EditUser => Driver.FindElement(By.LinkText("Edit"));

        public AddUsersPage AddUsersPage => new AddUsersPage(Driver);

        public EditUsersPage EditUsersPage => new EditUsersPage(Driver);

        public List<string> UserNames
        {
            get
            {
                var elements = Driver.FindElements(By.CssSelector("h1+ul>li"));

                return elements
                    .Select(x =>
                    {
                        var text = x.Text;
                        if (text.EndsWith("\r\nEdit\r\nDelete"))
                        {
                            text = text.Substring(0, text.Length - "\r\nEdit\r\nDelete".Length);
                        }
                        return text;
                    })
                    .ToList();
            }
        }

        public IWebElement GetEditLink(string userName)
        {
            ReadOnlyCollection<IWebElement> editLinks =
                Driver.FindElements(By.CssSelector("h1.title+ul>li"));

            return editLinks.Single(x => x.Text.StartsWith(userName)).FindElement(By.CssSelector("a.button"));
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
