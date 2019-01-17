using Microsoft.Data.Sqlite;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Tests.Services
{
    [TestClass]
    public class PairingServicesTest
    {
        private SqliteConnection SqliteConnection { get; set; }
        private ApplicationDbContext Options { get; set; }

        private Pairing CreatePairing()
        {

            User santa = new User
            {
                FirstName = "Alan",
                LastName = "Watts"
            };

            User recipient = new User
            {
                FirstName = "Rene",
                LastName = "Descartes"
            };

            Group group = new Group
            {
                Title = "Philosophers",
                UserGroups = new List<UserGroups>()
            };

            UserGroups santaBind = new UserGroups
            {
                User = santa,
                Group = group
            };

            UserGroups recipientBind = new UserGroups
            {
                User = recipient,
                Group = group
            };

            group.UserGroups.Add(santaBind);
            group.UserGroups.Add(recipientBind);

            Pairing pairing = new Pairing
            {
                Santa = santa,
                Recipient = recipient,
                Group = group
            };

            return pairing;
        }

    }
}
