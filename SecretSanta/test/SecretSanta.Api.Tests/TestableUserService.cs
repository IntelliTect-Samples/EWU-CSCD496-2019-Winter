using System.Collections.Generic;
using SecretSanta.Domain.Services;
using SecretSanta.Domain.Models;

namespace SecretSanta.Api.Tests
{
    public class TestableUserService : IUserService
    {
        public List<User> returnedUsers { get; set; }

        public User AddUser_User { get; set; }
        public User AddUser_Return { get; set; }

        public List<User> GetUsersIntoGroup_Return { get; set; }

        public User RemoveUser_User { get; set; }
        public User RemoveUser_Return { get; set; }

        public User UpdateUser_User { get; set; }
        public User UpdateUser_Return { get; set; }

        public User AddUser(User user)
        {
            AddUser_User = user;
            return AddUser_Return;
        }

        public List<User> FetchAll()
        {
            return GetUsersIntoGroup_Return;
        }

        public User RemoveUser(User user)
        {
            RemoveUser_User = user;
            return RemoveUser_Return;
        }

        public User UpdateUser(User user)
        {
            UpdateUser_User = user;
            return UpdateUser_Return;
        }
    }
}
