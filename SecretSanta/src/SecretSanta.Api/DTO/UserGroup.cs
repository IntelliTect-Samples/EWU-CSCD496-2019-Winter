using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.Api.DTO
{
    public class UserGroup
    {
        public int GroupId { get; set; }
        public Domain.Models.Group Group { get; set; }
        public int UserId { get; set; }
        public Domain.Models.User User { get; set; }

        public UserGroup()
        {

        }

        public UserGroup(Domain.Models.UserGroup userGroup)
        {
            if (userGroup == null) throw new ArgumentNullException(nameof(userGroup));

            GroupId = userGroup.GroupId;
            Group = userGroup.Group;
            UserId = userGroup.UserId;
            User = userGroup.User;
        }
    }
}
