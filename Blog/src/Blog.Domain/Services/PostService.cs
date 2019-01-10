using Blog.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Domain.Services
{
    public class PostService
    {
        private ApplicationDbContext DbContext { get; }
        public PostService(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public bool AddPost(Post postToAdd)
        {
            DbContext.Posts.Add(postToAdd);
            return DbContext.SaveChanges() > 0;
        }
    }
}
