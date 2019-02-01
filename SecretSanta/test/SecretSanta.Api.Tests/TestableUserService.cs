using System.Collections.Generic;
using System.Linq;
using SecretSanta.Domain.Services;
using SecretSanta.Domain.Models;

namespace SecretSanta.Api.Tests
{
    public class TestableUserService : IUserService
    {
        public List<User> UsersToReturn { get; set; }
        public User LastModifiedUser { get; set; }

        public User AddUser(User user)
        {
            LastModifiedUser = user;
            return user;
        }

        public User DeleteUser(User user)
        {
            LastModifiedUser = user;
            return user;
        }

        public User UpdateUser(User user)
        {
            LastModifiedUser = user;
            return user;
        }
    }
}