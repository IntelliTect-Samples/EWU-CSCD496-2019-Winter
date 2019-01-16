namespace SecretSanta.Domain.Models
{
    public class Pairing : Entity
    {
        public User Recipient { get; set; }
        public User Santa { get; set; }
    }
}