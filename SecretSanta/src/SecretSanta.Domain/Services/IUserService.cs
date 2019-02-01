using SecretSanta.Domain.Models;

namespace SecretSanta.Domain.Services
{
    public interface IUserService
    {
        User AddUser(User user);
        User UpdateUser(User user);


    }
}