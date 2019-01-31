using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.Api.DTO
{
    public class GroupUser
    {
        public int GroupId { get; set; }
        public SecretSanta.Domain.Models.Group Group { get; set; }
        public int UserId { get; set; }
        public SecretSanta.Domain.Models.User User { get; set; }

        public GroupUser()
        {

        }

        public GroupUser(SecretSanta.Domain.Models.GroupUser modelsGroupUser)
        {
            if (modelsGroupUser == null) throw new ArgumentNullException(nameof(modelsGroupUser));

            GroupId = modelsGroupUser.GroupId;
            Group = modelsGroupUser.Group;
            UserId = modelsGroupUser.UserId;
            User = modelsGroupUser.User;
        }

        public static SecretSanta.Domain.Models.GroupUser ToEntity(DTO.GroupUser dtoGroupUser)
        {
            //Pretend this is implemented
            return null;
        }
    }
}
