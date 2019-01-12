using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Models
{
    public class Pairing
    {
        public User UserFor { get; set; }
        public User Santa { get; set; }
    }
}
