﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;
using System.Threading.Tasks;

namespace SecretSanta.Domain.Tests.Services
{
    [TestClass]
    public class GroupServiceTests : DatabaseServiceTests
    {
        [TestMethod]
        public async Task AddGroup()
        {
            using (ApplicationDbContext context = new ApplicationDbContext(Options))
            {
                GroupService groupService = new GroupService(context);

                Group group = new Group
                {
                    Name = "Brute Squad"
                };

                await groupService.AddGroup(group).ConfigureAwait(false);

                Assert.AreNotEqual(0, group.Id);
            }
        }

        [TestMethod]
        public async Task UpdateUser()
        {
            using (ApplicationDbContext context = new ApplicationDbContext(Options))
            {
                UserService userService = new UserService(context);

                User user = new User
                {
                    FirstName = "Inigo",
                    LastName = "Montoya"
                };

                await userService.AddUser(user).ConfigureAwait(false);

                Assert.AreNotEqual(0, user.Id);
            }

            using (ApplicationDbContext context = new ApplicationDbContext(Options))
            {
                UserService userService = new UserService(context);
                User retrievedUser = await userService.GetById(1).ConfigureAwait(false);

                retrievedUser.FirstName = "Princess";
                retrievedUser.LastName = "Buttercup";

                await userService.UpdateUser(retrievedUser).ConfigureAwait(false);
            }

            using (ApplicationDbContext context = new ApplicationDbContext(Options))
            {
                UserService userService = new UserService(context);
                User retrievedUser = await userService.GetById(1).ConfigureAwait(false);

                Assert.AreEqual("Princess", retrievedUser.FirstName);
                Assert.AreEqual("Buttercup", retrievedUser.LastName);
            }
        }

        [TestMethod]
        public async Task DeleteUser()
        {
            using (ApplicationDbContext context = new ApplicationDbContext(Options))
            {
                UserService userService = new UserService(context);

                User user = new User
                {
                    FirstName = "Inigo",
                    LastName = "Montoya"
                };

                await userService.AddUser(user).ConfigureAwait(false);

                Assert.AreNotEqual(0, user.Id);
            }

            using (ApplicationDbContext context = new ApplicationDbContext(Options))
            {
                UserService userService = new UserService(context);
                bool isDeleted = await userService.DeleteUser(1).ConfigureAwait(false);
                Assert.IsTrue(isDeleted);
            }

            using (ApplicationDbContext context = new ApplicationDbContext(Options))
            {
                UserService userService = new UserService(context);
                User retrievedUser = await userService.GetById(1).ConfigureAwait(false);

                Assert.IsNull(retrievedUser);
            }
        }
    }
}