using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Tests.Models
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void CreateUser()
        {
            ICollection<Gift> gifts = new List<Gift>();
            ICollection<Group> groups = new List<Group>();

            User user = new User { FirstName = "Richard", LastName = "Teller", Gifts = gifts, Groups = groups };
            Assert.AreEqual("Richard", user.FirstName);
            Assert.AreEqual("Teller", user.LastName);
            Assert.AreEqual(gifts, user.Gifts);
            Assert.AreEqual(groups, user.Groups);
        }
        /*
        [TestMethod]
        public void UpdateUser()
        {
            ICollection<Gift> gifts = new List<Gift>();
            ICollection<Group> groups = new List<Group>();
            User user = User.AddUser("Richard", "Teller", gifts, groups);

            ICollection<Gift> gifts_new = new List<Gift>();
            ICollection<Group> groups_new = new List<Group>();
            user.UpdateUser("Richard_new", "Teller_new", gifts_new, groups_new);

            Assert.AreEqual("Richard_new", user.FirstName);
            Assert.AreEqual("Teller_new", user.LastName);
            Assert.AreEqual(gifts_new, user.Gifts);
            Assert.AreEqual(groups_new, user.Groups);
            
        }
        */
    }
}
