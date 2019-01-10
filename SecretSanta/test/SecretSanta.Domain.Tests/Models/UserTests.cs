using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Domain.Models;

namespace SecretSanta.Domain.Tests.Models
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void CanCreateUser()
        {
            var user = new User { FirstName = "Michael", LastName = "Stokesbary"};
            Assert.AreEqual("Michael", user.FirstName);
        }
    }
}