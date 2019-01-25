﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Domain.Models;
using SecretSanta.Import.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SecretSanta.Import.Tests
{
    [TestClass]
    public class GiftImportServiceTest
    {
        string dirPath;
        string tmpFilePath;

        [TestInitialize]
        public void SetUpDirectory()
        {
            string tempDirPath = Path.GetTempPath();
            dirPath = Path.Combine(tempDirPath + "SecretSantaGiftImport");

            if (Directory.Exists(dirPath))
                CleanUpDirectory();

            Directory.CreateDirectory(dirPath);
            tmpFilePath = Path.Combine(dirPath, "userGifts.tmp");
        }

        [TestCleanup]
        public void CleanUpDirectory()
        {
            DirectoryInfo dirInfo = new DirectoryInfo(dirPath);
            foreach (FileInfo file in dirInfo.GetFiles())
            {
                File.Delete(file.FullName);
            }
            Directory.Delete(dirPath);
        }

        [TestMethod]
        [DataRow(new[] { "Name: Grant Woods", "Gift1", "Gift2", "Gift3" })]
        [DataRow(new[] { "Name: Grant Woods", "", "Gift1", "  ", "Gift2", "", "Gift3", "" })]
        public void ImportGifts_ValidFormat_ReturnGiftCollection(string[] fileInputArray)
        {
            GiftImportService giftImportService = new GiftImportService();

            File.WriteAllLines(tmpFilePath, fileInputArray);

            ICollection<Gift> gifts = giftImportService.ImportGifts(tmpFilePath);
            List<Gift> wishList = new List<Gift>(gifts);
            Assert.AreEqual<string>("Grant", wishList[0].User.FirstName);
            Assert.AreEqual<string>("Woods", wishList[1].User.LastName);
            Assert.AreEqual<string>("Gift3", wishList[2].Title);

            File.Delete(tmpFilePath);
        }

        [TestMethod]
        [DataRow(new[] { "Name: Grant Woods", "", "", "" })]
        [DataRow(new[] { "Name: Grant Woods", "            ", " ", "" })]
        public void ImportGifts_ValidHeaderMissingGifts_ThrowsArgumentException(string[] fileInputArray)
        {
            GiftImportService giftImportService = new GiftImportService();

            File.WriteAllLines(tmpFilePath, fileInputArray);
            try
            {
                ICollection<Gift> gifts = giftImportService.ImportGifts(tmpFilePath);
                Assert.Fail();
            }
            catch (Exception exception)
            {
                Assert.AreEqual<Type>(typeof(ArgumentException), exception.GetType());
                File.Delete(tmpFilePath);
            }
        }

        [TestMethod]
        public void ImportGifts_InvalidFile_ThrowsFileNotFoundException()
        {
            try
            {
                GiftImportService giftImportService = new GiftImportService();
                ICollection<Gift> gifts = giftImportService.ImportGifts("THEFILE");
                Assert.Fail();
            }
            catch (Exception exception)
            {
                Assert.AreEqual<Type>(typeof(FileNotFoundException), exception.GetType());
            }
        }

        [TestMethod]
        public void ImportGifts_InvalidFileNull_ThrowsArgumentNullException()
        {
            try
            {
                GiftImportService giftImportService = new GiftImportService();
                ICollection<Gift> gifts = giftImportService.ImportGifts(null);
                Assert.Fail();
            }
            catch (Exception exception)
            {
                Assert.AreEqual<Type>(typeof(ArgumentNullException), exception.GetType());
            }
        }

        [TestMethod]
        [DataRow("")]
        [DataRow("   ")]
        public void ImportGifts_InvalidFileEmptyOrWhipeSpaceString_ThrowsArgumentException(string filePath)
        {
            try
            {
                GiftImportService giftImportService = new GiftImportService();
                ICollection<Gift> gifts = giftImportService.ImportGifts(filePath);
                Assert.Fail();
            }
            catch (Exception exception)
            {
                Assert.AreEqual<Type>(typeof(ArgumentException), exception.GetType());
            }
        }
    }
}