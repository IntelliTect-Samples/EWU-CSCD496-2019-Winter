using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Domain.Models;
using System;

namespace SecretSanta.Domain.Tests.Models
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
