using System.Collections.Generic;

namespace SecretSanta.Domain.Models
{
    public class User : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Gift> Gifts { get; set; }
        public List<Group> Groups { get; set; } // Could be multiple groups
    }
}