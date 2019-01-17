using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Models
{
    public class Gift : Entity
    {
        public string Title { set; get; }
        public int OrderOfImportance { set; get; }
        public string Url { set; get; }
        public string description { set; get; }
        public User User { set; get; }
    }
}
