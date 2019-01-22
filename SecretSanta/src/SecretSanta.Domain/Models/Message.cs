using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Models
{
    public class Message : Entity
    {
        public string Content { get; set; }
        public Pairing Pairing { get; set; }
        public int PairingId { get; set; }
    }
}
