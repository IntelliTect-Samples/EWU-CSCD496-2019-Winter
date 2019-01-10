using System.Collections.Generic;

namespace Blog.Domain.Models
{
    public class Tag : Entity
    {
        public string Name { get; set; }
        public ICollection<PostTag> PostTags { get; set; }
    }
}