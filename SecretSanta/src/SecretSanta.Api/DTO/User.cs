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
        public List<Domain.Models.Gift> Gifts { get; set; }
        public List<Domain.Models.GroupUser> GroupUsers { get; set; }

        public User()
        {

        }

        public User(SecretSanta.Domain.Models.User domainUser)
        {
            if (domainUser == null) throw new ArgumentNullException(nameof(domainUser));

            Id = domainUser.Id;
            FirstName = domainUser.FirstName;
            LastName = domainUser.LastName;
            Gifts = domainUser.Gifts;
            GroupUsers = domainUser.GroupUsers;
        }

        public static SecretSanta.Domain.Models.User ToEntity(DTO.User dtoUser)
        {
            if (dtoUser == null)
            {
                throw new ArgumentNullException(nameof(dtoUser));
            }

            Domain.Models.User entity = new Domain.Models.User
            {
                Id = dtoUser.Id,
                FirstName = dtoUser.FirstName,
                LastName = dtoUser.LastName,
                Gifts = dtoUser.Gifts,
                GroupUsers = dtoUser.GroupUsers
            };

            return entity;
        }
    }
}
