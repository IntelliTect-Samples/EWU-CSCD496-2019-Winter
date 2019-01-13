﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SecretSanta.Domain.Models
{
    public class Pairing : Entity
    {
        public Group Group { get; set; }
        public User Santa { get; set; }
        public User Recipient { get; set; }
    }
}
