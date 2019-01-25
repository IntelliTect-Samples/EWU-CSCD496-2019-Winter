using System;
using System.Collections.Generic;
using System.Text;

namespace src.Model
{
    public class Message : Entity
    {
        public Pairing Pairing { get; set; }
        public int PairingId { get; set; }

        public string MessagePost { get; set; }
    }
}
