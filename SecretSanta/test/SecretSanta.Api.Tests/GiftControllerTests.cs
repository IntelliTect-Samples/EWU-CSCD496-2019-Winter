using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Moq.AutoMock;
using SecretSanta.Domain.Services;

namespace SecretSanta.Api.Tests
{
    [TestClass]
    public class GiftControllerTests
    {
        protected AutoMocker AutoMocker { get; private set; }
        protected Mock<IGiftService> MockGiftService { get; private set; }

        [TestInitialize]
        public void InitializeMock()
        {
            AutoMocker = new AutoMocker();
            MockGiftService = AutoMocker.GetMock<IGiftService>();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GiftController_RequiresGiftService()
        {
            new GiftController(null);
        }
    }
}
