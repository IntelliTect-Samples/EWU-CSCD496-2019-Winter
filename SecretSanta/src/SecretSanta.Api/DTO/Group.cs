using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.Api.DTO
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<GroupUser> GroupUsers { get; set; }

        public Group()
        {

        }

        public Group(SecretSanta.Domain.Models.Group group)
        {
            if (group == null) throw new ArgumentNullException(nameof(group));

            Id = group.Id;
            Name = group.Name;

            List<Domain.Models.GroupUser> newGroup = new List<Domain.Models.GroupUser>();
            newGroup.AddRange(group.GroupUsers.Select(groupUser =>
            new Domain.Models.GroupUser
            {
                Group = groupUser.Group,
                GroupId = groupUser.GroupId,
                User = groupUser.User,
                UserId = groupUser.UserId
            }));
        }

        public static SecretSanta.Domain.Models.Group ToEntity(DTO.Group group)
        {
            //Pretend this is implemented
            return null;
        }
    }
}
