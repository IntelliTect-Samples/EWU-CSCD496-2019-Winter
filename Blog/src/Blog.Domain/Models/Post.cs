using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Domain.Models
{
    public class Post : Entity
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsPublished { get; set; }
        public string Slug { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
