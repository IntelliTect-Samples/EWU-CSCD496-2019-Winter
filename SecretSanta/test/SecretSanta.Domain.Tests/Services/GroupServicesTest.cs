using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Tests.Services
{
    [TestClass]
    public class GroupServicesTest
    {
        private SqliteConnection SqliteConnection { get; set; }
        private DbContextOptions<ApplicationDbContext> Options { get; set; }

        private Group CreateGroup(string title)
        {
            Group group = new Group
            {
                Title = "Family",
                UserGroups = new List<UserGroups>()
            };
            return group;
        }

        private User CreateUser(string f, string l)
        {
            return new User { FirstName = f, LastName = l };
        }

        private UserGroups CreateUserGroup(User user, Group group)
        {
            UserGroups userGroups = new UserGroups
            {
                User = user,
                UserId = user.Id,
                Group = group,
                GroupId = group.Id
            };
            return userGroups;
        }

        [TestInitialize]
        public void OpenConnection()
        {
            SqliteConnection = new SqliteConnection("DataSource=:memory:");
            SqliteConnection.Open();

            Options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(SqliteConnection)
                .Options;

            using (var context = new ApplicationDbContext(Options))
            {
                context.Database.EnsureCreated();
            }
        }

        [TestCleanup]
        public void CloseConnection()
        {
            SqliteConnection.Close();
        }

        [TestMethod]
        public void AddUserToGroup()
        {
            GroupService groupService;
            UserService userService;

            Group group = CreateGroup("Family");
            User user = CreateUser("Conner", "Verret");

            using (var context = new ApplicationDbContext(Options))
            {
                groupService = new GroupService(context);
                userService = new UserService(context);

                groupService.CreateGroup(group);
                userService.CreateUser(user);

                groupService.AddUserToGroup(user, 1);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                groupService = new GroupService(context);
                userService = new UserService(context);

                group = groupService.Find(1);
                user = userService.Find(1);

                Assert.AreSame(user, group.UserGroups[0].User);
            }
        }

        [TestMethod]
        public void CreateGroup_AddUser_ToGroup()
        {
            GroupService groupService;
            UserService userService;
            Group group = CreateGroup("Family");
            User user = CreateUser("Conner", "Verret");

            group.UserGroups.Add(CreateUserGroup(user, group));

            using (var context = new ApplicationDbContext(Options))
            {
                groupService = new GroupService(context);
                userService = new UserService(context);

                groupService.CreateGroup(group);
                userService.CreateUser(user);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                groupService = new GroupService(context);
                userService = new UserService(context);
                group = groupService.Find(1);
                user = userService.Find(1);

                Assert.AreEqual("Conner", group.UserGroups[0].User.FirstName);
                Assert.AreEqual("Family", user.UserGroups[0].Group.Title);
            }
        }

        [TestMethod]
        public void CreateGroup_AddAndRemoveUser_FromGroup()
        {
            GroupService groupService;
            UserService userService;
            Group group = CreateGroup("Family");

            User user1 = CreateUser("Conner", "Verret");
            User user2 = CreateUser("Carter", "Verret");
            User user3 = CreateUser("Paul", "Verret");

            group.UserGroups.Add(CreateUserGroup(user1, group));
            group.UserGroups.Add(CreateUserGroup(user2, group));
            group.UserGroups.Add(CreateUserGroup(user3, group));

            using (var context = new ApplicationDbContext(Options))
            {
                groupService = new GroupService(context);
                groupService.CreateGroup(group);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                groupService = new GroupService(context);
                groupService.RemoveUserFromGroup(group.Id, group.UserGroups[0].UserId);
                
            }

            using (var context = new ApplicationDbContext(Options))
            {
                groupService = new GroupService(context);
                userService = new UserService(context);
                group = groupService.Find(1);
                User user = userService.Find(1);

                User userStillInGroup = group.UserGroups[0].User;
                Assert.AreEqual(0, user.UserGroups.Count);
                Assert.AreEqual("Carter", userStillInGroup.FirstName);
                Assert.AreEqual(2, group.UserGroups.Count);
            }
        }

        [TestMethod]
        public void DeleteGroup()
        {
            GroupService groupService;
            Group group = CreateGroup("Philosophers");

            using (var context = new ApplicationDbContext(Options))
            {
                groupService = new GroupService(context);
                groupService.CreateGroup(group);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                groupService = new GroupService(context);
                groupService.DeleteGroup(group);
                Assert.IsNull(groupService.Find(group.Id));
            }
        }
    }
}
