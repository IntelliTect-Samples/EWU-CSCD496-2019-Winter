using System;
using System.Collections.Generic;
using System.Text;

namespace src.Model
{
    public class Pairing : Entity
    {
        public User Santa { get; set; }
        public User Recepiant { get; set; }
        //public List<Message> Messages { get; set; }
    }
}
