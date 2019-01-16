using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Domain.Models
{
    public class Post : Entity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PostedOn { get; set; }
        public bool IsPublished { get; set; }
        public string Slug { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public List<Comment> Comments { get; set; }

        public List<PostTag> PostTags { get; set; }
    }
}
