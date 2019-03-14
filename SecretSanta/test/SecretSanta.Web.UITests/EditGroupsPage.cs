﻿using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecretSanta.Web.UITests
{
    public class EditGroupsPage
    {
        IWebDriver Driver { get; }

        public IWebElement GroupNameTextBox => Driver.FindElement(By.Id("Name"));
        
        public string CurrentGroupID =>
            Driver.Url.Substring(Driver.Url.LastIndexOf("/") + 1);
        public IWebElement SubmitButton =>
            Driver
                .FindElements(By.CssSelector("button.is-primary"))
                .Single(x => x.Text == "Submit");

        public EditGroupsPage(IWebDriver driver)
        {
            Driver = driver ?? throw new ArgumentNullException(nameof(driver));
        }
    }
}
