using System;
using System.Collections.Generic;

namespace Blog.Domain.Models
{
    public class Post : Entity
    {
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Slug { get; set; }
        public DateTime AuthoredOn { get; set; }
        public bool IsPublished { get; set; }
        public ICollection<PostTag> PostTags { get; set; }
    }
}