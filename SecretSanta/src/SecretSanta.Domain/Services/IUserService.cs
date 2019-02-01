using System;
using System.Collections.Generic;
using System.Text;
using SecretSanta.Domain.Models;

namespace SecretSanta.Domain.Services
{
    public interface IUserService
    {
        User AddUser(User user);
        
        User UpdateUser(User user);

        User RemoveUser(User user);

        List<User> FetchAll();
    }
}
