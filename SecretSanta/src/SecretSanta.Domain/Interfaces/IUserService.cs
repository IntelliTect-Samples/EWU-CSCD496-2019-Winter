using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Interfaces
{
    public interface IUserService
    {
        bool UpsertUser(User user);
        bool DeleteUser(int id);
        User Find(int id);
        List<User> FetchAll();
    }
}
