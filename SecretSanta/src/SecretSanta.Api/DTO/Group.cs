using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.Api.DTO
{
    public class Group
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }

        public Group()
        { }

        public Group(SecretSanta.Domain.Models.Group group)
        {
            if (group == null)
            {
                throw new ArgumentNullException(nameof(group));
            }

            GroupId = group.Id;
            GroupName = group.Name;

        }

        public static Domain.Models.Group ToModelGroup(DTO.Group group)
        {
            return new Domain.Models.Group
            {
                Id = group.GroupId,
                Name = group.GroupName
            };
        }
    }
}
