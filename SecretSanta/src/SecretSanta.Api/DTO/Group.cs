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
        public List<Domain.Models.GroupUser> GroupUsers { get; set; }

        public Group()
        {

        }

        public Group(SecretSanta.Domain.Models.Group modelsGroup)
        {
            if (modelsGroup == null) throw new ArgumentNullException(nameof(modelsGroup));

            Id = modelsGroup.Id;
            Name = modelsGroup.Name;

            List<Domain.Models.GroupUser> newGroup = new List<Domain.Models.GroupUser>();
            newGroup.AddRange(modelsGroup.GroupUsers.Select(groupUser =>
            new Domain.Models.GroupUser
            {
                Group = groupUser.Group,
                GroupId = groupUser.GroupId,
                User = groupUser.User,
                UserId = groupUser.UserId
            }));
            GroupUsers = newGroup;
        }

        public static SecretSanta.Domain.Models.Group ToEntity(DTO.Group dtoGroup)
        {
            //Pretend this is implemented
            return null;
        }
    }
}
