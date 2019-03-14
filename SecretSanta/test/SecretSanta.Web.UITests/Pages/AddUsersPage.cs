﻿using System;
using System.Linq;
using OpenQA.Selenium;

namespace SecretSanta.Web.UITests.Pages
{
    public class AddUsersPage
    {
        public const string Slug = UsersPage.Slug + "/Add";

        public IWebDriver Driver { get; }

        public IWebElement UserFristNameTextBox => Driver.FindElement(By.Id("FirstName"));
        public IWebElement UserLastNameTextBox => Driver.FindElement(By.Id("LastName"));

        public IWebElement SubmitButton =>
            Driver
                .FindElements(By.CssSelector("button.is-primary"))
                .Single(x => x.Text == "Submit");

        public IWebElement CancelButton =>
            Driver
                .FindElements(By.CssSelector("a.button"))
                .Single(x => x.Text == "Cancel");

        public AddUsersPage(IWebDriver driver)
        {
            Driver = driver ?? throw new ArgumentNullException(nameof(driver));
        }
    }
}
