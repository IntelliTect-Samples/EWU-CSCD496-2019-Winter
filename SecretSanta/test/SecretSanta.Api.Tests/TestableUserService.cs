using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Api.Tests
{
    class TestableUserService : IUserService
    {
        public User AddUser_User { get; set; }
        public User ToReturn { get; set; }
        public User AddUser(User user)
        {
            AddUser_User = user;
            return ToReturn;
        }

        public List<User> ToReturnList { get; set; }
        public List<User> FetchAll()
        {
            return ToReturnList;
        }

        public User RemoveUser_User { get; set; }
        public User RemoveUser(User user)
        {
            RemoveUser_User = user;
            return ToReturn;
        }

        public User UpdateUser_User { get; set; }
        public User UpdateUser(User user)
        {
            UpdateUser_User = user;
            return ToReturn;
        }
    }
}
