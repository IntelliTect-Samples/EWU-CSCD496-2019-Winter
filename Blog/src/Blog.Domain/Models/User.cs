using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Domain.Models
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
