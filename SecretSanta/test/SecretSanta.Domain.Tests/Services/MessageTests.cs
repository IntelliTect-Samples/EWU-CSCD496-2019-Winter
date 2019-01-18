using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Tests.Services
{
    [TestClass]
    public class MessageTest : ApplicationServiceTest
    {
        Message CreateMessage()
        {
            return new Message
            {
                Content = "you killed my father, prepare to die",
                Sender = new User { FirstName = "Inigo" },
                Reciever = new User { FirstName = "Rugen" }
            };
        }

        [TestMethod]
        public void AddMessage()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new MessageService(context);
                var message = CreateMessage();

                var persitedMessage = service.AddMessage(message);

                Assert.AreNotEqual(0, persitedMessage.Id);

            }
        }

    }
}
