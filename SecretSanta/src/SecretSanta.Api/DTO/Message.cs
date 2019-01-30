using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.Api.DTO
{
    public class Message
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        [ForeignKey("SenderId")]
        public SecretSanta.Domain.Models.User Sender { get; set; }
        public int RecipientId { get; set; }
        [ForeignKey("RecipientId")]
        public SecretSanta.Domain.Models.User Recipient { get; set; }
        public string ChatMessage { get; set; }

        public Message()
        {

        }

        public Message(SecretSanta.Domain.Models.Message message)
        {
            Id = message.Id;
            Sender = message.Sender;
            SenderId = message.SenderId;
            Recipient = message.Recipient;
            RecipientId = message.RecipientId;
            ChatMessage = message.ChatMessage;
        }

        public static SecretSanta.Domain.Models.Message ToEntity(DTO.Message user)
        {
            //Pretend this is implemented
            return null;
        }

    }
}
