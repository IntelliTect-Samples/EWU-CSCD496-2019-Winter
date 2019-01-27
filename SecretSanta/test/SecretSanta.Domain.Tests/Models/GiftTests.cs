using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Tests.Models
{
    [TestClass]
    public class GiftTests
    {
        [TestMethod]
        public void CreateGift()
        {
            User user = new User
            {
                FirstName = "Conner",
                LastName = "Verret"
            };

            Gift gift = new Gift
            {
                Title = "Nintendo Switch",
                OrderOfImportance = 1,
                Description = "Nintendo's Premier Gaming Console.",
                Url = "nintendo.com",
                User = user,
            };

            Assert.AreEqual("Nintendo Switch", gift.Title);
        }
    }
}
