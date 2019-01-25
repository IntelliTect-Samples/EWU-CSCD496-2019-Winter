using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace SecretSanta.Library.Tests
{
    [TestClass]
    public class WishlistImporterTests
    {
        private static string TempPath = Path.GetTempPath(); 

        [ClassInitialize]
        public static void InitTest(TestContext testContext)
        {
            File.WriteAllLines($"{TempPath}/valid1", new[] { "Name: Wyatt Williams", "Legos", "Games"} );
            File.WriteAllLines($"{TempPath}/valid2", new[] { "Name: Williams, Wyatt", "Computer", "SNES"} ); 
            File.WriteAllLines($"{TempPath}/invalid", new[] { "" });
            File.WriteAllLines($"{TempPath}/invalidNameFormat", new[] { "Name:" });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FileNameisNull()
        {
            WishlistImporter wl = new WishlistImporter(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FileDoesNotExist()
        {
            WishlistImporter wl = new WishlistImporter("0123035C");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FileInvalidNameFormat()
        {
            WishlistImporter wl = new WishlistImporter($"{TempPath}/invalidNameFormat");
            User user = wl.GetUser();
        }

        [TestMethod]
        public void FileExists()
        {
            WishlistImporter wl = new WishlistImporter($"{TempPath}/valid1");
        }

        [TestMethod]
        public void GetValidUser1()
        {
            WishlistImporter wl = new WishlistImporter($"{TempPath}/valid1");
            User user = wl.GetUser();
            Assert.AreEqual("Wyatt", user.FirstName);
            Assert.AreEqual("Williams", user.LastName);
        }

        [TestMethod]
        public void GetValidUser2()
        {
            WishlistImporter wl = new WishlistImporter($"{TempPath}/valid2");
            User user = wl.GetUser();
            Assert.AreEqual("Wyatt", user.FirstName);
            Assert.AreEqual("Williams", user.LastName);
        }

        [TestMethod]
        public void GetValidUserGifts1()
        {
            WishlistImporter wl = new WishlistImporter($"{TempPath}/valid1");
            User user = wl.GetUser();
            List<Gift> gifts = wl.SetGifts(user);
            Assert.AreEqual("Legos", gifts[0].Title);
            Assert.AreEqual("Games", gifts[1].Title);
        }

        [TestMethod]
        public void GetValidUserGifts2()
        {
            WishlistImporter wl = new WishlistImporter($"{TempPath}/valid2");
            User user = wl.GetUser();
            List<Gift> gifts = wl.SetGifts(user);
            Assert.AreEqual("Computer", gifts[0].Title);
            Assert.AreEqual("SNES", gifts[1].Title);
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            File.Delete($"{TempPath}/valid1");
            File.Delete($"{TempPath}/valid2");
            File.Delete($"{TempPath}/invalid");
            File.Delete($"{TempPath}/invalidNameFormat");
        }
    }
}
