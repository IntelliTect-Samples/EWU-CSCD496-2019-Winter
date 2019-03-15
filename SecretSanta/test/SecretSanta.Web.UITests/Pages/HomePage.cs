using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;

namespace SecretSanta.Web.UITests.Pages
{
    public class HomePage
    {
        public const string Path = "https://localhost:44345/";
        public const string Slug = "";
        public IWebDriver WebDriver { get; set; }

        public HomePage(IWebDriver webDriver)
        {
            WebDriver = webDriver ?? throw new ArgumentNullException(nameof(webDriver));
        }

        public IWebElement GroupsLink => WebDriver.FindElement(By.LinkText("Groups"));

        public IWebElement UsersLink => WebDriver.FindElement(By.LinkText("Users"));

        public IWebElement GiftsLink => WebDriver.FindElement(By.LinkText("Gifts"));
    }
}
