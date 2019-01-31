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
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeleteGroup_RequireGroup()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new GroupService(context);
                service.DeleteGroup(null);
            }
        }

        [TestMethod]
        public void DeleteGroup_Success()
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
                
                service.DeleteGroup(group);
                
                Assert.AreEqual(0, context.Groups.Count());
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddUserToGroup_RequirePositiveGroupId()
        {
            var user = new User() { Id = 42 };
            
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new GroupService(context);

                service.AddUserToGroup(-1, user);
            }
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddUserToGroup_RequiresUser()
        {   
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new GroupService(context);

                service.AddUserToGroup(4, null);
            }
        }

        [TestMethod]
        public void AddUserToGroup_Success()
        {
            var user = new User { Id = 42 };
            var group = new Group { Id = 43 };
            var groupUser = new GroupUser { GroupId = group.Id, UserId = user.Id };
            group.GroupUsers = new List<GroupUser> { groupUser };
               
            var user2 = new User { Id = 45 };
              
            using (var context = new ApplicationDbContext(Options))
            {
                context.Users.Add(user);
                context.Groups.Add(group);
                context.SaveChanges();
            }
            
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new GroupService(context);

                User addedUser = service.AddUserToGroup(group.Id, user2);
                
                Assert.AreEqual(user2.Id, addedUser.Id);

                List<User> usersFromGroup = service.GetUsers(group.Id);
                
                Assert.IsTrue(usersFromGroup.Contains(user2));
            }
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RemoveUserFromGroup_RequirePositiveGroupId()
        {
            var user = new User() { Id = 42 };
            
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new GroupService(context);

                service.RemoveUserFromGroup(-1, user);
            }
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RemoveUserFromGroup_RequiresUser()
        {   
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new GroupService(context);

                service.RemoveUserFromGroup(4, null);
            }
        }
        
        [TestMethod]
        public void RemoveUserFromGroup_Success()
        {
            var user = new User { Id = 42 };
            var group = new Group { Id = 43 };
            var groupUser = new GroupUser { GroupId = group.Id, UserId = user.Id };
            group.GroupUsers = new List<GroupUser> { groupUser };
               
            var user2 = new User { Id = 45 };
              
            using (var context = new ApplicationDbContext(Options))
            {
                context.Users.Add(user);
                context.Groups.Add(group);
                context.SaveChanges();
            }
            
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new GroupService(context);

                User addedUser = service.RemoveUserFromGroup(group.Id, user2);
                
                Assert.AreEqual(user2.Id, addedUser.Id);

                List<User> usersFromGroup = service.GetUsers(group.Id);
                
                Assert.IsFalse(usersFromGroup.Contains(user2));
            }
        }
    }
}