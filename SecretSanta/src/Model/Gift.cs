using System;
using System.Collections.Generic;
using System.Text;

namespace src.Model
{
    public class Gift : Entity
    {
        public string Title { get; set; }
        public int OrderOfImportance { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
