using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Domain.Models
{
    public class Tag : Entity
    {
        public string Name { get; set; }

        public List<PostTag> PostTags { get; set; }
    }
}
