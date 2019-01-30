using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.Api.DTO
{
    public class Message
    {
        public int Id { get; set; }
        public Domain.Models.User UserTo { get; set; }
        public int UserToId { get; set; }
        public Domain.Models.User UserFrom { get; set; }
        public int UserFromId { get; set; }
        public string MessageBody { get; set; }

        public Message()
        {

        }

        public Message(Domain.Models.Message message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

            Id = message.Id;
            UserTo = message.UserTo;
            UserToId = message.UserToId;
            UserFrom = message.UserFrom;
            UserFromId = message.UserFromId;
            MessageBody = message.MessageBody;
        }
    }
}
