using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Models
{
    public class Group : Entity
    {
        public string Title { get; set; }
        public ICollection<UserGroup> UserGroups { get; set; }

        public Group()
        {
            UserGroups = new List<UserGroup>();
            EntityType = "Group";
        }

        public bool UserIsPartOf(User user)
        {
            bool test = false;

            List<UserGroup> list = (List<UserGroup>)UserGroups;
            for(int index = 0; index < UserGroups.Count; index++)
            {
                if (list[index].UserId == user.Id)
                    test = true;
            }

            return test;
        }
    }
}
