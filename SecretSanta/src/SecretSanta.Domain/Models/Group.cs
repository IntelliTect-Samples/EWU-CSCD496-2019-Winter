using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Models
{
    public class Group
    {
        public string Titel { get; set; }
        public ICollection<User> UserList { get; set; }
    }
}
