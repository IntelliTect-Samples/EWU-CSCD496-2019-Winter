namespace SecretSanta.Domain.Models
{
    public class Message : Entity
    {
        public User Recipient { get; set; }
        public User Sender { get; set; }
        public string Text { get; set; }
    }
}