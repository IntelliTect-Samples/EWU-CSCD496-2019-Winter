using System.Collections.Generic;

namespace Blog.Domain.Models
{
    public class Author : Entity
    {
        public string Name { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}