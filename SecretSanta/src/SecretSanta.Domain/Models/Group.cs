using System.Collections.Generic;

namespace SecretSanta.Domain.Models
{
    public class Group : Entity
    {
        public string Title { get; set; }
        public List<User> Users { get; set; }
    }
}