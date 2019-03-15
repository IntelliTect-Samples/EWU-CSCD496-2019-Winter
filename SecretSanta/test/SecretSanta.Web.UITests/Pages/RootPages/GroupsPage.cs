using OpenQA.Selenium;
using SecretSanta.Web.UITests.Pages.AddPages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SecretSanta.Web.UITests.Pages.RootPages
{
    public class GroupsPage
    {
        public const string Slug = "Groups";

        public IWebDriver Driver { get; }

        public IWebElement AddGroup => Driver.FindElement(By.LinkText("Add Group"));

        public GroupsPageAdd AddGroupsPage => new GroupsPageAdd(Driver);

        public List<string> GroupNames
        {
            get
            {
                ReadOnlyCollection<IWebElement> elements = Driver.FindElements(By.CssSelector("h1+ul>li"));

                return elements
                    .Select(x =>
                    {
                        string text = x.Text;
                        if (text.EndsWith(" Edit Delete"))
                        {
                            text = text.Substring(0, text.Length - " Edit Delete".Length);
                        }
                        return text;
                    })
                    .ToList();
            }
        }

        public IWebElement GetDeleteLink(string groupName)
        {
            ReadOnlyCollection<IWebElement> deleteLinks =
                Driver.FindElements(By.CssSelector("a.is-danger"));

            IWebElement deleteLink = deleteLinks.Single(x => x.GetAttribute("onclick").EndsWith($"{groupName}?')"));
            return deleteLink;
        }

        public GroupsPage(IWebDriver driver)
        {
            Driver = driver ?? throw new ArgumentNullException(nameof(driver));
        }

        internal IWebElement GetEditLink(string userName)
        {
            return Driver.FindElements(By.PartialLinkText("Edit")).Last();
        }
    }
}
