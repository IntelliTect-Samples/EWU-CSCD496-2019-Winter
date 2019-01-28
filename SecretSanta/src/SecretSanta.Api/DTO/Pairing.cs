using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.Api.DTO
{
    public class Pairing
    {
        public Domain.Models.User UserFor { get; set; }
        public int UserForId { get; set; }
        public Domain.Models.User Santa { get; set; }
        public int SantaId { get; set; }

        public Pairing()
        {

        }

        public Pairing(Domain.Models.Pairing pairing)
        {
            if (pairing == null) throw new ArgumentNullException(nameof(pairing));

            UserFor = pairing.UserFor;
            UserForId = pairing.UserForId;
            Santa = pairing.Santa;
            SantaId = pairing.SantaId;
        }
    }
}
