using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Models
{
    class Pairing
    {
        User Recipient { set; get; }
        User Santa { set; get; }
    }
}
