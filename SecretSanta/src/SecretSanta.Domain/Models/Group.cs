using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Models
{
    public class Group : Entity
    {
        public string Title { get; set; }
        public List<UserGroups> UserGroups { get; set; }
    }
}
