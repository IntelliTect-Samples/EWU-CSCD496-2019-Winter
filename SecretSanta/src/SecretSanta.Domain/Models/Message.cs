using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Models
{
    public class Message : Entity
    {
        public User ToUser { get; set; }
        public User FromUser { get; set; }
        public string Content { get; set; }
    }
}
