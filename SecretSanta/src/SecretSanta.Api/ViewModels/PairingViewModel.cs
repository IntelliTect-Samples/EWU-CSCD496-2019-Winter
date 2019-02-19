﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SecretSanta.Domain.Models;

namespace SecretSanta.Api.ViewModels
{
    public class PairingViewModel
    {
        public int Id { get; set; }
        public int SantaId { get; set; }
        public int RecipientId { get; set; }
    }
}
