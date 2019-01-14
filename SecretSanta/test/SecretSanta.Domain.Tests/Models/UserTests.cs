﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            User user = new User { FirstName = "Edmond", LastName = "Dantes" };
            Assert.AreEqual("Edmond", user.FirstName);

        }
    }
}
