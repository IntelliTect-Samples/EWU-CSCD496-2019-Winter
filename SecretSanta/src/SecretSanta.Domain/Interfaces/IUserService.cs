using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Interfaces
{
    public interface IUserService
    {
        void UpsertUser(User user);
        User Find(int id);
        List<User> FetchAll();
    }
}
