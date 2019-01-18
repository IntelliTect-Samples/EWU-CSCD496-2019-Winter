namespace SecretSanta.Domain.Models
{
    public class Pairing : Entity
    {
        public User Recipient { set; get; }
        public User Santa { set; get; }
    }
}