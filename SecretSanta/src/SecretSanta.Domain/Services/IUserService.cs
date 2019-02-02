using SecretSanta.Domain.Models;
using System.Collections.Generic;

namespace SecretSanta.Domain.Services
{
    public interface IUserService
    {
        List<User> FetchAll();
        User AddUser(User user);
        User UpdateUser(User user);
        bool DeleteUser(int userId);
    }
}