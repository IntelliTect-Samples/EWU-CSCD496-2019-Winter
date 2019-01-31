using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.Api.DTO
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public List<Gift> Gifts { get; set; }
        //public List<UserGroups> UserGroups { get; set; }

        public User(Domain.Models.User user)
        {
            if (user is null) throw new ArgumentNullException(nameof(user));

            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            //Gifts = user.Gifts;
            //UserGroups = user.UserGroups;
        }

        public static Domain.Models.User ToEntity(User user)
        {
            return new Domain.Models.User
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName//,
                //Gifts = user.Gifts,
                //UserGroups = user.UserGroups
            };
        }
    }
}
