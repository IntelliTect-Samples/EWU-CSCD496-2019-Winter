using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;

namespace SecretSanta.Api.DTO
{
    public class Group
    {
        public string Title { get; set; }
        public ICollection<Domain.Models.UserGroup> UserGroups { get; set; }

        public Group()
        {

        }

        public Group(Domain.Models.Group group)
        {
            if (group == null) throw new ArgumentNullException(nameof(group));

            Title = group.Title;
            UserGroups = group.UserGroups;
        }
    }
}
