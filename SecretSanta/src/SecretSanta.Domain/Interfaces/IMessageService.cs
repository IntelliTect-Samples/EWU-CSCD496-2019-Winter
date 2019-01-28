using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Interfaces
{
    public interface IMessageService
    {
        void StoreMassage(Message message);
        Message FindMessage(int id);
    }
}
