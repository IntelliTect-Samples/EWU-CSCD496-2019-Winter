﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Models
{
    //hold the title, order of importance, url, description, and User
    public class Gift : Entity
    {
        public string Title { get; set; }
        public int OrderOfImportance { get; set; }
        public string URL { get; set; }
        public string Description { get; set; }
        public User User { get; set; }
    }
}
