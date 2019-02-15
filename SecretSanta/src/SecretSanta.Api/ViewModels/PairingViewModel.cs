using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.Api.ViewModels
{
    public class PairingViewModel
    {
        public int Id { get; set; }
        public int SantaId { get; set; }
        public UserViewModel Santa { get; set; }
        public int RecipientId { get; set; }
        public UserViewModel Recipient { get; set; }
    }
}
