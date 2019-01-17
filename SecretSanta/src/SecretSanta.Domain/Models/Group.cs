using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Models
{
    public class Group : Entity
    {
        public string Title { get; set; }

        // Commenting out Users to avoid any many-to-many connections.  Will change at a later date.
        // public ICollection<User> Users { get; set; } 
    }
}
