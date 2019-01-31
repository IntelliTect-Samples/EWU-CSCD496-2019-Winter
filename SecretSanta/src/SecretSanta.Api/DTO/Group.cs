using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;

namespace SecretSanta.Api.DTO
{
    public class Group
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<Domain.Models.UserGroup> UserGroups { get; set; }

        public Group()
        {

        }

        public Group(Domain.Models.Group group)
        {
            if (group == null) throw new ArgumentNullException(nameof(group));

            Id = group.Id;
            Title = group.Title;
            UserGroups = group.UserGroups;
        }

        public static Domain.Models.Group GetDomainGroup(DTO.Group group)
        {
            Domain.Models.Group mGroup = new Domain.Models.Group()
            {
                Title = group.Title,
                Id = group.Id,
                UserGroups = group.UserGroups
            };

            return mGroup;
        }
    }
}
