using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.Api.DTO
{
    public class Pairing
    {
        public int Id { get; set; }
        public int SantaId { get; set; }
        [ForeignKey("SantaId")]
        public SecretSanta.Domain.Models.User Santa { get; set; }
        public int RecipientId { get; set; }
        [ForeignKey("RecipientId")]
        public SecretSanta.Domain.Models.User Recipient { get; set; }

        public Pairing()
        {

        }

        public Pairing(SecretSanta.Domain.Models.Pairing pairing)
        {
            Id = pairing.Id;
            SantaId = pairing.SantaId;
            Santa = pairing.Santa;
            RecipientId = pairing.RecipientId;
            Recipient = pairing.Recipient;
        }

        public static SecretSanta.Domain.Models.Pairing ToEntity(DTO.Pairing user)
        {
            //Pretend this is implemented
            return null;
        }
    }
}
