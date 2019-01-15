using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Domain.Models;

namespace SecretSanta.Domain.Tests
{
    [TestClass]
    public class Class1
    {
        [TestMethod]
        public void MakeUser()
        {
            User testUser = new User { First = "Miles", Last = "Prower" };
            Assert.AreEqual("Miles", testUser.First);
            //Assert.AreEqual("User", testUser.EntityType);
        }
    }
}
