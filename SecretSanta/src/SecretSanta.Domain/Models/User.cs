using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Models
{
    public class User : Entity
    {
        public string First { get; set; }
        public string Last { get; set; }
        public ICollection<UserGroup> Groups { get; set; }
        public ICollection<Gift> Gifts { get; set; }
        
        public User()
        {
            EntityType = "User";
        }
    }
}
