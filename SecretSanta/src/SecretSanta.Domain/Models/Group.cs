using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Models
{
    public class Group : Entity
    {
        public string Title { set; get; }
        public List<UserGroup> UserGroups { set; get; }

    }
}
