using System;
using System.Collections.Generic;
using System.Text;

namespace BlogEngine.Domain.Models
{
    public class Post : Entity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PostedOn { get; set; }
        public bool IsPublished { get; set; }
        public int AuthorId { get; set; }
        public User Author { get; set; }
        public string Slug { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<PostTag> PostTags { get; set; }
    }
}
