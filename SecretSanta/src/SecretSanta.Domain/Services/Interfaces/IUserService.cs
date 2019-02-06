using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Services.Interfaces
{
    public interface IUserService
    {
        User Find(int id);
        User AddUser(User user);

        User UpdateUser(User user);

        List<User> FetchAll();

        bool DeleteUser(int userId);
        User GetUser(int id);
    }
}
