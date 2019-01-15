using System;
using System.Collections.Generic;
using System.Text;

namespace src.Models
{
    public class Message : Entity
    {
        public User Santa { get; set; }
        public User Recipient { get; set; }

        public string MessagePost { get; set; }

    }
}
