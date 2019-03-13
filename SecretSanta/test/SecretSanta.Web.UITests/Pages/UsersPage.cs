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
                        if (text.EndsWith("\r\nEdit\r\nDelete"))
                        {
                            text = text.Substring(0, text.Length - "\r\nEdit\r\nDelete".Length);
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

            return deleteLinks.Single(x => x.GetAttribute("onclick").EndsWith($"{userName}?')"));
        }

        public UsersPage(IWebDriver driver)
        {
            Driver = driver ?? throw new ArgumentNullException(nameof(driver));
        }
    }
}
