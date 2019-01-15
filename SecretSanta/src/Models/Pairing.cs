using System;
using System.Collections.Generic;
using System.Text;

namespace src.Models
{
    public class Pairing : Entity
    {
        public User Santa { get; set; }
        public User Recepiant { get; set; }

        public Pairing(User santa, User recepiant)
        {
            Santa = santa;
            Recepiant = recepiant;
        }
    }
}
