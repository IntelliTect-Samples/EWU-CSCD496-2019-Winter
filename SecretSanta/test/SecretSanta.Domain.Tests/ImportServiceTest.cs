using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Domain.Models;
using SecretSanta.Import;
using System;
using System.Collections.Generic;
using System.IO;

namespace SecretSanta.Domain.Tests
{
    [TestClass]
    public class ImportServiceTest
    {
        private SqliteConnection SqliteConnection { get; set; }
        private DbContextOptions<SecretSantaDbContext> Options { get; set; }

        [TestInitialize]
        public void MakeTestFiles()
        {
            BuildTestFile("inputFile.txt", "Name: Brad Howard");
            BuildTestFile("inputFile2.txt", "Name: Howard, Brad");
            BuildTestFile("inputFile3.txt", "Name:");
            BuildTestFile("inputFile4.txt", "Name");
            BuildTestFile("inputFile5.txt", "Name: Howard");
            BuildTestFile("inputFile6.txt", "Name: Howard,");
            BuildTestFile("inputFile7.txt", "Name:Howard,");
            BuildTestFile("inputFile8.txt", "Name: Brad Howard", "T-64BM2", "", "M60A3 TTS", "F-14D Tomcat");
            OpenConnection();
        }

        public void OpenConnection()
        {
            SqliteConnection = new SqliteConnection("DataSource=:memory:");
            SqliteConnection.Open();

            Options = new DbContextOptionsBuilder<SecretSantaDbContext>().UseSqlite(SqliteConnection).Options;

            using (var context = new SecretSantaDbContext(Options))
            {
                context.Database.EnsureCreated();
            }
        }

        [TestCleanup]
        public void CloseConnection()
        {
            SqliteConnection.Close();
            CleanupFiles();
        }

        public void CleanupFiles()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            File.Delete(Path.Combine(Path.GetTempPath(), "inputFile.txt"));
            File.Delete(Path.Combine(Path.GetTempPath(), "inputFile2.txt"));
            File.Delete(Path.Combine(Path.GetTempPath(), "inputFile3.txt"));
            File.Delete(Path.Combine(Path.GetTempPath(), "inputFile4.txt"));
            File.Delete(Path.Combine(Path.GetTempPath(), "inputFile5.txt"));
            File.Delete(Path.Combine(Path.GetTempPath(), "inputFile6.txt"));
            File.Delete(Path.Combine(Path.GetTempPath(), "inputFile7.txt"));
            File.Delete(Path.Combine(Path.GetTempPath(), "inputFile8.txt"));
        }

        public void BuildTestFile(string fileName, params string[] args)
        {
            string path = Path.Combine(Path.GetTempPath(), fileName);

            if (!File.Exists(path))
            {
                using (StreamWriter ostream = File.CreateText(path))
                {
                    foreach (string s in args)
                    {
                        ostream.WriteLine(s);
                    }
                    ostream.Close();
                }
            }
        }

        [TestMethod]
        public void OpenFileForImport_CanRead()
        {
            SecretSantaDbContext dbContext = new SecretSantaDbContext(Options);
            string path = Path.Combine(Path.GetTempPath(), "inputFile.txt");
            ImportService importService = new ImportService(path, dbContext);
            
            Assert.AreEqual<bool>(true, !importService.Istream.EndOfStream);
        }

        [TestMethod]
        public void ReadFileForImport_ReadName()
        {
            SecretSantaDbContext dbContext = new SecretSantaDbContext(Options);
            string path = Path.Combine(System.IO.Path.GetTempPath(), "inputFile.txt");
            ImportService importService = new ImportService(path, dbContext);

            Assert.AreEqual<string>("Name: Brad Howard", importService.ReadLine());

            importService.Dispose();
        }

        [TestMethod]
        public void ParseName_FirstNameFirst()
        {
            SecretSantaDbContext dbContext = new SecretSantaDbContext(Options);
            string path = Path.Combine(Path.GetTempPath(), "inputFile.txt");
            ImportService importService = new ImportService(path, dbContext);

            Assert.AreEqual<string>("Brad", importService.ParseName(importService.ReadLine())[0]);

            importService.Dispose();
        }

        [TestMethod]
        public void ParseName_LastNameFirst()
        {
            SecretSantaDbContext dbContext = new SecretSantaDbContext(Options);
            string path = Path.Combine(Path.GetTempPath(), "inputFile2.txt");
            ImportService importService = new ImportService(path, dbContext);

            Assert.AreEqual<string>("Brad", importService.ParseName(importService.ReadLine())[0]);

            importService.Dispose();
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ParseName_MissingNameAfterColon()
        {
            SecretSantaDbContext dbContext = new SecretSantaDbContext(Options);
            string path = Path.Combine(Path.GetTempPath(), "inputFile3.txt");
            ImportService importService = new ImportService(path, dbContext);

            Assert.AreEqual<string>("Brad", importService.ParseName(importService.ReadLine())[0]);

            importService.Dispose();
        }

        [TestMethod]
        public void BuildGiftList_PullGiftsFromFile()
        {
            SecretSantaDbContext dbContext = new SecretSantaDbContext(Options);
            string path = Path.Combine(Path.GetTempPath(), "inputFile8.txt");
            ImportService importService = new ImportService(path, dbContext);

            string temp = importService.ReadLine();

            if (temp == "Name: Brad Howard")
            {
                Assert.AreEqual<int>(3, importService.BuildGiftList().Count);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void PasrseName_MalformedInput()
        {
            SecretSantaDbContext dbContext = new SecretSantaDbContext(Options);
            string path = Path.Combine(Path.GetTempPath(), "inputFile4.txt");
            ImportService importService = new ImportService(path, dbContext);

            Assert.AreEqual<string>("Brad", importService.ParseName(importService.ReadLine())[0]);

            importService.Dispose();
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void PasrseName_MissingLastName()
        {
            SecretSantaDbContext dbContext = new SecretSantaDbContext(Options);
            string path = Path.Combine(Path.GetTempPath(), "inputFile5.txt");
            ImportService importService = new ImportService(path, dbContext);

            Assert.AreEqual<string>("Brad", importService.ParseName(importService.ReadLine())[0]);

            importService.Dispose();
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ParseName_MissingFirstName()
        {
            SecretSantaDbContext dbContext = new SecretSantaDbContext(Options);
            string path = Path.Combine(Path.GetTempPath(), "inputFile6.txt");
            ImportService importService = new ImportService(path, dbContext);

            Assert.AreEqual<string>("Brad", importService.ParseName(importService.ReadLine())[0]);

            importService.Dispose();
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void ReadFileForImport_FileNotFound()
        {
            SecretSantaDbContext dbContext = new SecretSantaDbContext(Options);
            string path = Path.Combine(Path.GetTempPath(), "inputFile9.txt");
            ImportService importService = new ImportService(path, dbContext);

            Assert.AreEqual<string>("Brad", importService.ParseName(importService.ReadLine())[0]);

            importService.Dispose();
        }

        [TestMethod]
        public void PlaceUser_IsThere()
        {
            SecretSantaDbContext dbContext = new SecretSantaDbContext(Options);
            User user = new User() { First = "Brad", Last = "Howard" };
            string path = Path.Combine(Path.GetTempPath(), "inputFile.txt");
            ImportService importService = new ImportService(path, dbContext);

            importService.PlaceUser(user);

            Assert.AreEqual<string>("Brad", importService.UserService.Find(1).First);

            importService.Dispose();
        }

        [TestMethod]
        public void BuildUser_NoGifts()
        {
            SecretSantaDbContext dbContext = new SecretSantaDbContext(Options);
            string[] name = { "Brad", "Howard" };
            string[] gifts = { };

            string path = Path.Combine(Path.GetTempPath(), "inputFile.txt");
            ImportService importService = new ImportService(path, dbContext);

            Assert.AreEqual<string>("Brad", importService.BuildUser(name, gifts).First);

            importService.Dispose();
        }

        [TestMethod]
        public void BuildUser_WithGifts()
        {
            SecretSantaDbContext dbContext = new SecretSantaDbContext(Options);
            string[] name = { "Brad", "Howard" };
            string[] gifts = { "T-64BM2", "M60A3 TTS", "F-14D Super Tomcat"};

            List<Gift> list = new List<Gift>();

            foreach(string s in gifts)
            {
                list.Add(new Gift() { Title = s });
            }

            string path = Path.Combine(Path.GetTempPath(), "inputFile.txt");
            ImportService importService = new ImportService(path, dbContext);

            Assert.AreEqual<int>(3, importService.BuildUser(name, list).Gifts.Count);

            importService.Dispose();
        }

        [TestMethod]
        public void Dispose_Called()
        {
            SecretSantaDbContext dbContext = new SecretSantaDbContext(Options);
            string path = Path.Combine(Path.GetTempPath(), "inputFile.txt");
            ImportService importService;

            using (importService = new ImportService(path, dbContext))
            {
                Assert.IsNotNull(importService.Istream);
            }

            Assert.IsNull(importService.Istream);
        }
    }
}