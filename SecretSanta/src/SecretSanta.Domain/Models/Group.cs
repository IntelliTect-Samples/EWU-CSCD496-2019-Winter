using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecretSanta.Domain.Models
{
    public class Group : Entity
    {
        public string Title { get; set; }
        [NotMapped] public List<User> Users { get; set; } // Ignoring the many-many connection for now
    }
}