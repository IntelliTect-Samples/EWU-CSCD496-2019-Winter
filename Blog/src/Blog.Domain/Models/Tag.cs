using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Domain.Models
{
    public class Tag : Entity
    {
        public string Name { get; set; }

        public ICollection<PostTag> PostTags { get; set; }
    }
}
