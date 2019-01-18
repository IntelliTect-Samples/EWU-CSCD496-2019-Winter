using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Models
{
    public class Message : Entity
    {
        public String MessageContent { set; get; }
        public User Recipiant { get; set; }
        public User Santa { get; set; }
    }
}
