using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.Api.DTO
{
    public class User
    {
        public int UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }

        public User()
        {}

        public User(Domain.Models.User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
                
            UserFirstName = user.FirstName;
            UserLastName = user.LastName;
        }

        public static Domain.Models.User ToModelUser(DTO.User user)
        {
            return new Domain.Models.User
            {
                Id = user.UserId,
                FirstName = user.UserFirstName,
                LastName = user.UserLastName,
            };
        }
    }
}
