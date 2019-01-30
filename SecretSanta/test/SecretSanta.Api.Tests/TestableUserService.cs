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
        public int DeleteUser_UserId { get; set; }
        public User User { get; set; }

        public List<User> GetUsersForGroup(int groupId)
        {
            GetUsersForGroup_GroupId = groupId;
            return ToReturn;
        }

        public User CreateUser(User user)
        {
            if (user is null) throw new ArgumentNullException(nameof(user));
            return user;
        }

        public User UpdateUser(User user, int userId)
        {
            if (user is null) throw new ArgumentNullException(nameof(user));
            return user;
        }

        public User DeleteUser(int userId)
        {
            DeleteUser_UserId = userId;
            return User;
        }
    }
}
