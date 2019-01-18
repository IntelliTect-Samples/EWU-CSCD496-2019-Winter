using System.Collections.Generic;

namespace SecretSanta.Domain.Models
{
    public class Group : Entity
    {
        public string Title { set; get; }
        public List<User> Members { set; get; }
    }
}