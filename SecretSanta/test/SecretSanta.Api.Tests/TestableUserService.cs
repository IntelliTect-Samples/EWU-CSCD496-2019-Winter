using SecretSanta.Domain.Interfaces;
using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Api.Tests
{
    public class TestableUserService : IUserService
    {
        public List<User> ToReturn { get; set; }
        public int UserId { get; set; }

        public bool DeleteUser(int id)
        {
            return true;
        }

        public List<User> FetchAll()
        {
            return ToReturn;
        }

        public User Find(int id)
        {
            UserId = id;

            if (ToReturn == null)
            {
                ToReturn = new List<User>
                {
                    new User() { Id = id }
                };
            }

            return ToReturn[0];
        }

        public bool MakeUser(string info)
        {
            UserId = 1;

            return true;
        }

        public bool UpsertUser(User user)
        {
            return true;
        }
    }
}
