using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using OpenQA.Selenium;

namespace SecretSanta.Web.UITests.Pages
{
    public class UsersPage
    {
        public const string Path = HomePage.Path + "Users/";
        public const string Slug = "Users";
        public IWebDriver WebDriver { get; set; }

        public UsersPage(IWebDriver webDriver)
        {
            WebDriver = webDriver ?? throw new ArgumentNullException(nameof(webDriver));
        }

        public IWebElement AddUserLink => WebDriver.FindElement(By.LinkText("Add User"));

        public List<string> UserNames
        {
            get
            {
                var elements = WebDriver.FindElements(By.CssSelector("h1+ul>li"));
                return elements
                    .Select(x =>
                    {
                        var text = x.Text;

                        if (text.EndsWith(" Edit Delete"))
                        {
                            text = text.Substring(0, text.Length - " Edit Delete".Length);
                        }
                        return text;
                    }).ToList();
            }
        }

        public IWebElement GetDeleteLink(string userName)
        {
            ReadOnlyCollection<IWebElement> deleteLinks = WebDriver.FindElements(By.CssSelector("a.is-danger"));
            return deleteLinks.Single(x => x.GetAttribute("onclick").EndsWith($"{userName}?')"));
        }

        public IWebElement GetEditLink(string userName)
        {
            ReadOnlyCollection<IWebElement> editLinks = WebDriver.FindElements(By.CssSelector("h1+ul>li"));
            return editLinks.Single(x => x.Text.StartsWith(userName)).FindElement(By.CssSelector("a.button"));
        }


    }
}
