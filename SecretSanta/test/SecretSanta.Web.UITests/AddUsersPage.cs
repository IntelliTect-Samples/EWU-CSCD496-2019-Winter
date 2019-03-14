﻿using System;
using System.Linq;
using OpenQA.Selenium;

namespace SecretSanta.Web.UITests
{
    public class AddUsersPage
    {
        public const string Slug = UsersPage.Slug + "/Add";
        public IWebDriver Driver { get; }
        public IWebElement UserFirstNameTextBox => Driver.FindElement(By.Id("FirstName"));
        public IWebElement UserLastNameTextBox => Driver.FindElement(By.Id("LastName"));

        public IWebElement SubmitButton =>
            Driver
                .FindElements(By.CssSelector("button.is-primary"))
                .Single(x => x.Text == "Submit");

        public AddUsersPage(IWebDriver driver)
        {
            Driver = driver ?? throw new ArgumentNullException(nameof(driver));
        }
    }
}