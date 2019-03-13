using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;

namespace SecretSanta.Web.UITests
{
    class HomePage
    {
        public IWebDriver Driver { get; }

        public IWebElement UsersLink => Driver.FindElement(By.LinkText("Users"));

        public HomePage(IWebDriver driver)
        {
            Driver = driver ?? throw new ArgumentNullException(nameof(driver));
        }
    }
}