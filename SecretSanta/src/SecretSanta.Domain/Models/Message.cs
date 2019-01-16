﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Models
{
    public class Message : Entity
    {
        public User UserTo { get; set; }
        public User UserFrom { get; set; }
        public string MessageBody { get; set; }

        public Message()
        {
            EntityType = "Message";
        }
    }
}
