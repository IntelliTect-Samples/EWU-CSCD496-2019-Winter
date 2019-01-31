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
            List<Domain.Models.Gift> copyGifts = new List<Domain.Models.Gift>();
            copyGifts.AddRange(domainUser.Gifts.Select(gift =>
            new Domain.Models.Gift
            {
                Id = gift.Id,
                Description = gift.Description,
                Title = gift.Title,
                OrderOfImportance = gift.OrderOfImportance,
                Url = gift.Url,
                User = gift.User,
                UserId = gift.UserId
            }));
            Gifts = copyGifts;
            List<Domain.Models.GroupUser> copyGroup = new List<Domain.Models.GroupUser>();
            copyGroup.AddRange(domainUser.GroupUsers.Select(groupUser =>
            new Domain.Models.GroupUser
            {
                Group = groupUser.Group,
                GroupId = groupUser.GroupId,
                User = groupUser.User,
                UserId = groupUser.UserId
            }));
            GroupUsers = copyGroup;
        }

        public static SecretSanta.Domain.Models.User ToEntity(DTO.User dtoUser)
        {
            if (dtoUser == null)
            {
                throw new ArgumentNullException(nameof(dtoUser));
            }

            List<Domain.Models.Gift> copyGifts = new List<Domain.Models.Gift>();
            copyGifts.AddRange(dtoUser.Gifts.Select(gift =>
            new Domain.Models.Gift
            {
                Id = gift.Id,
                Description = gift.Description,
                Title = gift.Title,
                OrderOfImportance = gift.OrderOfImportance,
                Url = gift.Url,
                User = gift.User,
                UserId = gift.UserId
            }));

            List<Domain.Models.GroupUser> copyGroup = new List<Domain.Models.GroupUser>();
            copyGroup.AddRange(dtoUser.GroupUsers.Select(groupUser =>
            new Domain.Models.GroupUser
            {
                Group = groupUser.Group,
                GroupId = groupUser.GroupId,
                User = groupUser.User,
                UserId = groupUser.UserId
            }));

            Domain.Models.User entity = new Domain.Models.User
            {
                Id = dtoUser.Id,
                FirstName = dtoUser.FirstName,
                LastName = dtoUser.LastName,
                Gifts = copyGifts,
                GroupUsers = copyGroup
            };

            return entity;
        }
    }
}
