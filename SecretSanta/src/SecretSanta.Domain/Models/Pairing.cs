using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Models
{
    public class Pairing : Entity
    {
        public User Santa { get; set; }
        public int SantaId { get; set; }
        public User Recipient { get; set; }
        public int RecipientId { get; set; }
        public List<Message> Messages { get; set; }

        public Pairing()
        {
            Messages = new List<Message>();
        }
    }
}
