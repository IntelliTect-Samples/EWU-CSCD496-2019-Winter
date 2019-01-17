using Microsoft.Data.Sqlite;
using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Tests.Services
{
    public class MessageServicesTest
    {
        private SqliteConnection SqliteConnection { get; set; }
        private ApplicationDbContext DbContext { get; set; }
        
    }
}
