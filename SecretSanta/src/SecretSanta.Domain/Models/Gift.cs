using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Models
{
    public class Gift
    {
        public string Title { get; set; }
        public int WantTier { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
        public User WhoWantIt { get; set; }
    }
}
