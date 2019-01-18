using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Tests.Services
{
    [TestClass]
    public class UserServiceTests : ApplicationServiceTest
    {

        User CreateUser()
        {
            var user = new User
            {
                FirstName = "Inigo",
                LastName = "Montoya"
            };
            return user;
        }

        [TestMethod]
        public void AddUser()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                UserService service = new UserService(context);
                var myUser = CreateUser();

                var persistedUser = service.AddUser(myUser);

                Assert.AreNotEqual(0, persistedUser.Id);
            }
        }
        
        [TestMethod]
        public void UpdateUser()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                UserService service = new UserService(context);
                var myUser = CreateUser();
                var persistedUser = service.AddUser(myUser);

                persistedUser.FirstName = "asdf";
                service.UpdateUser(persistedUser);

                Assert.AreEqual(myUser.Id, persistedUser.Id);
                Assert.AreNotEqual("Inigo", persistedUser.FirstName);
            }
        }


    }
}
