using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Web.UITests.Pages
{
    public class GroupsPage
    {
        public const string Path = HomePage.Path + "Groups/";
        public const string Slug = "Groups";
        public IWebDriver WebDriver { get; set; }

        public GroupsPage(IWebDriver webDriver)
        {
            WebDriver = webDriver ?? throw new ArgumentNullException(nameof(webDriver));
        }


        //
    }
}
