using System;
using System.Collections.Generic;
using System.Text;

namespace src.Model
{
    public class Pairing : Entity
    {
        public User Santa { get; set; }
        public int SantaId { get; set; }
        public User Recepiant { get; set; }
        public int RecepiantId { get; set; }
        public List<Message> Messages { get; set; }
    }
}
