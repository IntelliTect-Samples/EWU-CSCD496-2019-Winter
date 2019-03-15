using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Web.UITests.Pages
{
    public class GiftPage
    {
        public const string Path = HomePage.Path + "Gifts/";
        public const string Slug = "Gifts";
        public IWebDriver WebDriver { get; set; }

        public GiftPage(IWebDriver webDriver)
        {
            WebDriver = webDriver ?? throw new ArgumentNullException(nameof(webDriver));
        }
    }
}
