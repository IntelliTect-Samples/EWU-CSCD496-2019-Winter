using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;
using System;
using System.Collections.Generic;

namespace SecretSanta.Api.Tests
{
    public class TestableUserService : IUserService
    {
        public List<User> ToReturn { get; set; }
        public int GetUsersForGroup_GroupId { get; set; }

        public List<User> GetUsersForGroup(int groupId)
        {
            GetUsersForGroup_GroupId = groupId;
            return ToReturn;
        }

        public User CreateUser_User { get; set; }
        public User CreateUser(User user)
        {
            if (user is null) throw new ArgumentNullException(nameof(user));
            CreateUser_User = user;
            return CreateUser_User;
        }

        public User UpdateUser_User { get; set; }
        public int UpdateUser_UserId { get; set; }
        public User UpdateUser(User user, int userId)
        {
            if (user is null) throw new ArgumentNullException(nameof(user));
            UpdateUser_User = user;
            UpdateUser_UserId = userId;
            return UpdateUser_User;
        }

        public User DeleteUser_User { get; set; }
        public User DeleteUser(User user)
        {
            if (user is null) throw new ArgumentNullException(nameof(user));
            DeleteUser_User = user;
            return DeleteUser_User;
        }
    }
}
