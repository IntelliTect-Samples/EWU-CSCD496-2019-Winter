using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Models
{
    public class Group : Entity
    {
        public string Title { get; set; }
        public ICollection<UserGroup> Users { get; set; }

        public Group()
        {
            EntityType = "Group";
        }
    }
}
