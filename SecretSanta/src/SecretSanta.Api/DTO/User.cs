using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.Api.DTO
{
    public class User
    {
        public int Id { get; set; }
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

            Id = user.Id;
            First = user.First;
            Last = user.Last;
            UserGroups = user.UserGroups;
            Gifts = user.Gifts;
        }

        public static Domain.Models.User GetDomainUser(User user)
        {
            Domain.Models.User mUser = new Domain.Models.User()
            {
                Id = user.Id,
                First = user.First,
                Last = user.Last,
                Gifts = user.Gifts,
                UserGroups = user.UserGroups
            };

            return mUser;
        }
    }
}
