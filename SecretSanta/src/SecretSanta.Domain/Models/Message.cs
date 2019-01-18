using System.Collections.Generic;

namespace SecretSanta.Domain.Models
{
    public class Message : Entity
    {
        public User Sender { set; get; }
        public User Reciever { set; get; }
        public string Content { set; get; }
        //int MessageId { set; get; }
    }
}