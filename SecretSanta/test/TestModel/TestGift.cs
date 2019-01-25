using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.Model;
using src.Models;
using src.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace test.TestModel
{
    [TestClass]
    public class TestGift
    {
        [TestMethod]
        public void Valid_GiftNotEqual_ReturnFalse()
        {
            Gift gift1 = new Gift { Title = "It" };
            Gift gift2 = new Gift { Title = "Cujo" };
            Assert.IsFalse(gift1.Equals(gift2));
        }

        [TestMethod]
        public void Valid_GiftEqual_ReturnTrue()
        {
            Gift gift1 = new Gift { Title = "Paul Sheldon" };
            Gift gift2 = new Gift { Title = "Paul Sheldon" };
            Assert.IsTrue(gift1.Equals(gift2));
        }

        [TestMethod]
        public void Valid_GiftNotEqualRightSide_ReturnFalse()
        {
            Gift gift1 = new Gift { Title = "Paul Sheldon" };
            Gift gift2 = null;
            Assert.IsFalse(gift1.Equals(gift2));
        }
    }
}
