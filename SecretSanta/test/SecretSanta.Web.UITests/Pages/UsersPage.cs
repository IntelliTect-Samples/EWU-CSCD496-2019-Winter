using System;
using System.Collections.Generic;
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
    }
}
