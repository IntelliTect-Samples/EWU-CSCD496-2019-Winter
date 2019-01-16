using System.Collections.Generic;

namespace SecretSanta.Domain
{
    public class Message
    {
        string Content { set; get; }

        public Message(string message)
        {
            Content = message;
        }



    }
}