using System.ComponentModel.DataAnnotations.Schema;

namespace SecretSanta.Domain.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        [ForeignKey("SenderId")]
        public User Sender { get; set; }
        public int RecipientId { get; set; }
        [ForeignKey("RecipientId")]
        public User Recipient { get; set; }
        public string ChatMessage { get; set; }
    }
}