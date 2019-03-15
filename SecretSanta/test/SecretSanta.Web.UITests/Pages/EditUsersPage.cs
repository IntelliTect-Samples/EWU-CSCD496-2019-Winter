using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Web.UITests.Pages
{
    public class EditUsersPage
    {
        public const string Path = UsersPage.Path + "Edit/";
        public const string Slug = UsersPage.Slug + "/Edit";

        public IWebDriver WebDriver { get; set; }

        public EditUsersPage(IWebDriver webDriver)
        {
            WebDriver = webDriver ?? throw new ArgumentNullException(nameof(webDriver));
        }

        //
    }
}
