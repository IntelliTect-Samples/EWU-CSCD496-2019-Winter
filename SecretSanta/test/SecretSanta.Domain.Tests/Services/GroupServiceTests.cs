using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
            var group = new Group
            {
                Name = "Test Group"
            };
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new GroupService(context);

                Group addedGroup = service.AddGroup(group);
                Assert.AreEqual(addedGroup, group);
                Assert.AreNotEqual(0, addedGroup.Id);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                Group retrievedGroup = context.Groups.Single();
                Assert.AreEqual(group.Name, retrievedGroup.Name);
            }
        }

        [TestMethod]
        public void UpdateGroup_UpdatesExistingGroup()
        {
            var group = new Group
            {
                Name = "Test Group"
            };
            using (var context = new ApplicationDbContext(Options))
            {
                context.Groups.Add(group);
                context.SaveChanges();
            }

            group.Name = "Updated Name";
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new GroupService(context);
                Group updatedGroup = service.UpdateGroup(group);
                Assert.AreEqual(group, updatedGroup);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                Group retrievedGroup = context.Groups.Single();
                AssertAreEqual(group, retrievedGroup);
            }
        }

        [TestMethod]
        public void FetchAll_ReturnsAllGroups()
        {
            var group1 = new Group
            {
                Name = "Group1"
            };
            var group2 = new Group
            {
                Name = "Group1"
            };
            using (var context = new ApplicationDbContext(Options))
            {
                context.Groups.Add(group1);
                context.Groups.Add(group2);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(Options))
            {
                var service = new GroupService(context);

                List<Group> groups = service.FetchAll();

                Assert.AreEqual(2, groups.Count);
                AssertAreEqual(group1,groups[0]);
                AssertAreEqual(group2,groups[1]);
            }
        }

        [TestMethod]
        public void GetUsers_ReturnsUserInGroup()
        {
            var user = new User { Id = 42 };
            var group = new Group { Id = 43 };
            var groupUser = new GroupUser { GroupId = group.Id, UserId = user.Id };
            group.GroupUsers = new List<GroupUser> { groupUser };

            using (var context = new ApplicationDbContext(Options))
            {
                context.Users.Add(user);
                context.Groups.Add(group);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(Options))
            {
                var service = new GroupService(context);
                List<User> users = service.GetUsers(43);
                Assert.AreEqual(42, users.Single().Id);
            }
        }

        [TestMethod]
        public void GetUsers_ReturnsEmptySetWhenGroupIsNotFound()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new GroupService(context);
                List<User> users = service.GetUsers(4);
                Assert.AreEqual(0, users.Count);
            }
        }

        [TestMethod]
        public void DeleteGroup_ReturnsFalseWhenNotFound()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new GroupService(context);

                bool result = service.DeleteGroup(1);

                Assert.IsFalse(result);
            }
        }

        [TestMethod]
        public void DeleteGroup_RemovesExistingGroup()
        {
            var group = new Group
            {
                Name = "Group1"
            };
            using (var context = new ApplicationDbContext(Options))
            {
                context.Groups.Add(group);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(Options))
            {
                var service = new GroupService(context);

                bool result = service.DeleteGroup(group.Id);

                Assert.IsTrue(result);
                Assert.IsFalse(context.Groups.Any());
            }
        }


        [TestMethod]
        public void AddUserToGroup_ReturnsFalseWhenGroupIsNotFound()
        {
            var user = new User
            {
                FirstName = "John",
                LastName = "Doe",
            };
            using (var context = new ApplicationDbContext(Options))
            {
                context.Users.Add(user);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(Options))
            {
                var service = new GroupService(context);

                bool result = service.AddUserToGroup(1, user.Id);

                Assert.IsFalse(result);
            }
        }

        [TestMethod]
        public void AddUserToGroup_ReturnsFalseWhenUserIsNotFound()
        {
            var group = new Group
            {
                Name = "Group1"
            };
            using (var context = new ApplicationDbContext(Options))
            {
                context.Groups.Add(group);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(Options))
            {
                var service = new GroupService(context);

                bool result = service.AddUserToGroup(group.Id, 1);

                Assert.IsFalse(result);
            }
        }

        [TestMethod]
        public void AddUserToGroup_SuccessfullyAdds()
        {
            var group = new Group
            {
                Name = "Group1"
            };
            var user = new User
            {
                FirstName = "John",
                LastName = "Doe",
            };
            using (var context = new ApplicationDbContext(Options))
            {
                context.Users.Add(user);
                context.Groups.Add(group);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(Options))
            {
                var service = new GroupService(context);

                bool result = service.AddUserToGroup(group.Id, user.Id);

                Assert.IsTrue(result);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                Group updatedGroup = context.Groups
                    .Include(x => x.GroupUsers)
                    .FirstOrDefault(x => x.Id == group.Id);

                Assert.AreEqual(1, updatedGroup.GroupUsers.Count);
                Assert.AreEqual(group.Id, updatedGroup.GroupUsers[0].GroupId);
                Assert.AreEqual(user.Id, updatedGroup.GroupUsers[0].UserId);
            }
        }

        [TestMethod]
        public void RemoveUserFromGroup_SuccessfullyRemoves()
        {
            var user = new User { Id = 42 };
            var group = new Group { Id = 43 };
            var groupUser = new GroupUser { GroupId = group.Id, UserId = user.Id };
            group.GroupUsers = new List<GroupUser> { groupUser };

            using (var context = new ApplicationDbContext(Options))
            {
                context.Users.Add(user);
                context.Groups.Add(group);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(Options))
            {
                var service = new GroupService(context);

                bool result = service.RemoveUserFromGroup(group.Id, user.Id);

                Assert.IsTrue(result);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                Group updatedGroup = context.Groups
                    .Include(x => x.GroupUsers)
                    .FirstOrDefault(x => x.Id == group.Id);
                
                Assert.AreEqual(0, updatedGroup.GroupUsers.Count);
            }
        }

        [TestMethod]
        public void RemoveUserFromGroup_ReturnsFalseWhenGroupIsNotFound()
        {
            var user = new User { Id = 42 };

            using (var context = new ApplicationDbContext(Options))
            {
                context.Users.Add(user);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(Options))
            {
                var service = new GroupService(context);

                bool result = service.RemoveUserFromGroup(1, user.Id);

                Assert.IsFalse(result);
            }
        }

        [TestMethod]
        public void RemoveUserFromGroup_ReturnsFalseWhenUserIsNotFound()
        {
            var group = new Group { Id = 43 };

            using (var context = new ApplicationDbContext(Options))
            {
                context.Groups.Add(group);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(Options))
            {
                var service = new GroupService(context);

                bool result = service.RemoveUserFromGroup(group.Id, 1);

                Assert.IsFalse(result);
            }
        }

        private static void AssertAreEqual(Group expected, Group actual)
        {
            if (expected == null && actual == null) return;
            if (expected == null || actual == null) Assert.Fail();

            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Name, actual.Name);
        }
    }
}