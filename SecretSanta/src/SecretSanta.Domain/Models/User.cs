using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Models
{
    public class User : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Gift> Gifts { get; set; }
        public ICollection<Group> Groups { get; set; }
    }
}
