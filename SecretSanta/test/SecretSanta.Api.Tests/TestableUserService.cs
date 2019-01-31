using System.Collections.Generic;
using SecretSanta.Domain.Services;
using SecretSanta.Domain.Models;

namespace SecretSanta.Api.Tests
{
    public class TestableUserService : IUserService
    {
        public List<User> ReturnedUsers { get; set; }
        public int AddUser_UserId { get; set; }
        public User AddUser_User { get; set; }
        public User AddUser_Return { get; set; }

        public List<User> GetUsersIntoGroup_Return { get; set; }
        public int RemoveUser_UserId { get; set; }
        public User RemoveUser_User { get; set; }
        public User RemoveUser_Return { get; set; }
        public int UpdateUser_UserId { get; private set; }
        public User UpdateUser_User { get; set; }
        public User UpdateUser_Return { get; set; }

        public User AddUser(int userId, User user)
        {
            AddUser_UserId = userId;
            AddUser_User = user;
            return AddUser_Return;
        }

        public List<User> FetchAll()
        {
            return GetUsersIntoGroup_Return;
        }

        public User RemoveUser(int userId, User user)
        {
            RemoveUser_UserId = userId;
            RemoveUser_User = user;
            return RemoveUser_Return;
        }

        public User UpdateUser(int userId, User user)
        {
            UpdateUser_UserId = userId;
            UpdateUser_User = user;
            return UpdateUser_Return;
        }
    }
}
