using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Domain.Models
{
    public class User : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Post> Posts { get; set; }
    }
}
