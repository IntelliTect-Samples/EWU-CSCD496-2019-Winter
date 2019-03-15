using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Web.UITests.Pages
{
    public class AddGroupPage
    {
        public const string Path = GroupsPage.Path + "Add/";
        public const string Slug = GroupsPage.Slug + "/Add";

        public IWebDriver WebDriver { get; set; }

        public AddGroupPage(IWebDriver webDriver)
        {
            WebDriver = webDriver ?? throw new ArgumentNullException(nameof(webDriver));
        }

        //
    }
}
