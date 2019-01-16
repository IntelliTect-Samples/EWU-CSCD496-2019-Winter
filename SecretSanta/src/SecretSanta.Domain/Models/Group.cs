using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Models
{
    public class Group : Entity
    {
        public string Title { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
