using SecretSanta.Domain.Models;
using System.Collections.Generic;

namespace SecretSanta.Domain.Services
{
    public interface IUserService
    {
        User AddUser(int userId, User user);
        User UpdateUser(int userId, User user);
        List<User> FetchAll();
        User RemoveUser(int userId, User user);
    }
}
