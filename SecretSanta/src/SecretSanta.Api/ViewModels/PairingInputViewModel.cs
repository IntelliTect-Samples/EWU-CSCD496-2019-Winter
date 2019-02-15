using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.Api.ViewModels
{
    public class PairingInputViewModel
    {
        public int SantaId { get; set; }
        public int RecipientId { get; set; }
    }
}
