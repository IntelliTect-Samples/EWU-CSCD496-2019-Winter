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

        public User(SecretSanta.Domain.Models.User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            List<Domain.Models.Gift> copyGifts = new List<Domain.Models.Gift>();
            copyGifts.AddRange(user.Gifts.Select(gift =>
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
            copyGroup.AddRange(user.GroupUsers.Select(groupUser =>
            new Domain.Models.GroupUser
            {
                Group = groupUser.Group,
                GroupId = groupUser.GroupId,
                User = groupUser.User,
                UserId = groupUser.UserId
            }));
            GroupUsers = copyGroup;
        }

        public static SecretSanta.Domain.Models.User ToEntity(DTO.User user)
        {
            //Pretend this is implemented
            return null;
        }
    }
}
