using SecretSanta.Domain.Models;
using System.Collections.Generic;

namespace SecretSanta.Domain.Services
{
    public interface IUserService
    {
        List<User> GetUsersForGroup(int groupId);
        User CreateUser(User user);
        User UpdateUser(User user, int userId);
        User DeleteUser(User user);
        User Find(int userId);
    }
}
