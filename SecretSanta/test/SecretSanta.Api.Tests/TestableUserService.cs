using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Api.Tests
{
    public class TestableUserService : IUserService
    {
        public User Find_UserId { get; set; }
        public User AddUser_User { get; set; }
        public bool DeleteUser_Bool { get; set; }
        public List<User> FetchUsers_List { get; set; }
        public User UpdateUser_User { get; set; }

        public User Find(int id)
        {
            Find_UserId = new User
            {
                Id = id
            };
            return Find_UserId;
        }
        public User AddUser(User user)
        {
            if (user is null) throw new ArgumentNullException(nameof(user));
            AddUser_User = user;
            return AddUser_User;
        }

        public bool DeleteUser(int userId)
        {
            if (userId > 0) DeleteUser_Bool = true;
            return DeleteUser_Bool;
        }

        public List<User> FetchAll()
        {
            FetchUsers_List = new List<User>();
            return FetchUsers_List;
        }

        public User UpdateUser(User user)
        {
            if (user is null) throw new ArgumentNullException(nameof(user));
            UpdateUser_User = user;
            return UpdateUser_User;
        }
    }
}
