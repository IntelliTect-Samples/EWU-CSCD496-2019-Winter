using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Models
{
    public class Message
    {
        public User UserFor { get; set; }
        public User UserFrom { get; set; }
        public string MessageBody { get; set; }
    }
}
