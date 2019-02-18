using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.Domain.Tests.Services
{
    [TestClass]
    public class PairingServiceTests
    {
        /*[TestInitialize]
        public async Task Initialize()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                var groupService = new GroupService(context);
                var userService = new UserService(context);

                var user = new User
                {
                    FirstName = "one",
                    LastName = "uno"
                };

                var user2 = new User
                {
                    FirstName = "two",
                    LastName = "dos"
                };

                var user3 = new User
                {
                    FirstName = "three",
                    LastName = "tres"
                };

                user = await userService.AddUser(user);
                user2 = await userService.AddUser(user2);
                user3 = await userService.AddUser(user3);

                var group = new Group
                {
                    Name = "Gorup1"
                };

                group = await groupService.AddGroup(group);

                await groupService.AddUserToGroup(group.Id, user.Id);
                await groupService.AddUserToGroup(group.Id, user2.Id);
                await groupService.AddUserToGroup(group.Id, user3.Id);
            }

        }

        [TestMethod]
        public void MyTestMethod()
        {

        }*/
    }
}
