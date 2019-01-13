using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blog.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Domain.Tests.Models
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void CreateUser()
        {
            User user = new User { FirstName = "Inigo", LastName = "Montoya" };
            Assert.AreEqual("Inigo", user.FirstName);
        }
    }
}
