using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SecretSanta.Web.UITests.Pages.AddPages;
using SecretSanta.Web.UITests.Pages.RootPages;

namespace SecretSanta.Web.UITests.PagesTests
{
    [TestClass]
    public class GroupsPageTests : AbstractParentTest
    {
        [TestMethod]
        public void CanGetToGroupsPage()
        {
            //Arrange
            Driver.Navigate().GoToUrl(RootUrl);

            //Act
            HomePage homePage = new HomePage(Driver);
            homePage.GroupsLink.Click();

            //Assert
            Assert.IsTrue(Driver.Url.EndsWith(GroupsPage.Slug));
        }

        [TestMethod]
        public void CanNavigateToAddGroupsPage()
        {
            //Arrange
            Uri rootUri = new Uri(RootUrl);
            Driver.Navigate().GoToUrl(new Uri(rootUri, GroupsPage.Slug));
            GroupsPage page = new GroupsPage(Driver);

            //Act
            page.AddGroup.Click();

            //Assert
            Assert.IsTrue(Driver.Url.EndsWith(GroupsPageAdd.Slug));
        }

        [TestMethod]
        public void CanAddGroups()
        {
            //Arrange /Act
            string groupName = "Group Name " + Guid.NewGuid().ToString("N");
            GroupsPage page = CreateGroup(groupName);

            //Assert
            Assert.IsTrue(Driver.Url.EndsWith(GroupsPage.Slug));
            List<string> groupNames = page.GroupNames;
            Assert.IsTrue(groupNames.Contains(groupName));
        }

        [TestMethod]
        public void CanEditGroups()
        {
            //Arrange /Act
            string groupName = "Group Name " + Guid.NewGuid().ToString("N");
            GroupsPage page = CreateGroup(groupName);

            string editGroupName = "Edit " + groupName;
            IWebElement editLink = page.GetEditLink(groupName);
            editLink.Click();

            GroupsPageAdd editGroupPage = new GroupsPageAdd(Driver);
            editGroupPage.GroupNameTextBox.Clear();
            editGroupPage.GroupNameTextBox.SendKeys(editGroupName);
            editGroupPage.SubmitButton.Click();

            //Assert
            Assert.IsTrue(Driver.Url.EndsWith(GroupsPage.Slug));

            List<string> groupNames = page.GroupNames;
            Assert.IsTrue(groupNames.Contains(editGroupName));
        }

        [TestMethod]
        public void CanDeleteGroup()
        {
            //Arrange
            string groupName = "Group Name " + Guid.NewGuid().ToString("N");
            GroupsPage page = CreateGroup(groupName);

            //Act
            IWebElement deleteLink = page.GetDeleteLink(groupName);
            deleteLink.Click();

            Driver.SwitchTo().Alert().Accept();

            //Assert
            List<string> groupNames = page.GroupNames;
            Assert.IsFalse(groupNames.Contains(groupName));
        }

        private GroupsPage CreateGroup(string groupName)
        {
            Uri rootUri = new Uri(RootUrl);
            Driver.Navigate().GoToUrl(new Uri(rootUri, GroupsPage.Slug));
            GroupsPage page = new GroupsPage(Driver);
            page.AddGroup.Click();

            GroupsPageAdd addGroupPage = new GroupsPageAdd(Driver);

            addGroupPage.GroupNameTextBox.SendKeys(groupName);
            addGroupPage.SubmitButton.Click();
            return page;
        }
    }
}