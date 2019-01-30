using System.ComponentModel.DataAnnotations.Schema;

namespace SecretSanta.Domain.Models
{
    public class Pairing
    {
        public int Id { get; set; }
        public int SantaId { get; set; }

        [ForeignKey("SantaId")] public User Santa { get; set; }

        public int RecipientId { get; set; }

        [ForeignKey("RecipientId")] public User Recipient { get; set; }
    }
}