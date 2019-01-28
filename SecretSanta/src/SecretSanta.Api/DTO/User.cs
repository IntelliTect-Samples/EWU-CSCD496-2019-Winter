using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.Api.DTO
{
    public class User
    {
        public string First { get; set; }
        public string Last { get; set; }
        public ICollection<Domain.Models.UserGroup> UserGroups { get; set; }
        public ICollection<Domain.Models.Gift> Gifts { get; set; }

        public User()
        {

        }

        public User(Domain.Models.User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            First = user.First;
            Last = user.Last;
            UserGroups = user.UserGroups;
            Gifts = user.Gifts;
        }
    }
}
