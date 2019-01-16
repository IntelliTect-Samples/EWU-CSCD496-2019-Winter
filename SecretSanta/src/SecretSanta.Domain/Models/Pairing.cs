using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Models
{
    public class Pairing : Entity
    {
        public User UserFor { get; set; }
        public int UserForId { get; set; }
        public User Santa { get; set; }
        public int SantaId { get; set; }

        public Pairing()
        {
            EntityType = "Pairing";
        }
    }
}
