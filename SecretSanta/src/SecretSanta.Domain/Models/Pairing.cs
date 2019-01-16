using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Models
{
    public class Pairing
    {
        User Recipient { set; get; }
        User Santa { set; get; }
    }
}
