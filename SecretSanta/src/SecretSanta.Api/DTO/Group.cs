using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.Api.DTO
{
    public class Group
    {
        public int Id { get; set; }
        public string Title { get; set; }
        // public List<UserGroups> UserGroups { get; set; }

        public Group(SecretSanta.Domain.Models.Group group)
        {
            if (group is null) throw new ArgumentNullException(nameof(group));

            Id = group.Id;
            Title = group.Title;
            //UserGroups = group.UserGroups;
        }   // Might need a method that converts a Domain.Models.UserGroup into a DTO.Usergroup...

        public static Group ToDTO(SecretSanta.Domain.Models.Group group)
        {
            return new Group(group);
        }
    }
}
