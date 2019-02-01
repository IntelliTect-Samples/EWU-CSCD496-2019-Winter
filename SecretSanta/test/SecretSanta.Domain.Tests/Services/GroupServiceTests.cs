using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;
using System;
using System.Linq;

namespace SecretSanta.Domain.Tests.Services
{
    [TestClass]
    public class GroupServiceTests : DatabaseServiceTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GroupService_RequiresDbContext()
        {
            new GroupService(null);
        }

        [TestMethod]
        public void AddGroup_PersistsGroup()
        {
            var @group = new Group
            {
                Name = "Test Group"
            };
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new GroupService(context);

                Group addedGroup = service.AddGroup(@group);
                Assert.AreEqual(addedGroup, @group);
                Assert.AreNotEqual(0, addedGroup.Id);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                Group retrievedGroup = context.Groups.Single();
                Assert.AreEqual(@group.Name, retrievedGroup.Name);
            }
        }

        [TestMethod]
        public void UpdateGroup_UpdatesExistingGroup()
        {
            var @group = new Group
            {
                Name = "Test Group"
            };
            using (var context = new ApplicationDbContext(Options))
            {
                context.Groups.Add(@group);
                context.SaveChanges();
            }

            @group.Name = "Updated Name";
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new GroupService(context);
                Group updatedGroup = service.UpdateGroup(@group);
                Assert.AreEqual(@group, updatedGroup);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                Group retrievedGroup = context.Groups.Single();
                Assert.AreEqual(@group.Id, retrievedGroup.Id);
                Assert.AreEqual(@group.Name, retrievedGroup.Name);
            }
        }
    }
}