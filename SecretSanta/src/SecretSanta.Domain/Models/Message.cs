using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Models
{
    public class Message : Entity
    {
        public User UserTo { get; set; }
        public int UserToId { get; set; }
        public User UserFrom { get; set; }
        public int UserFromId { get; set; }
        public string MessageBody { get; set; }
    }
}
