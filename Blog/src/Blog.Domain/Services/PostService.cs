using Microsoft.EntityFrameworkCore;
using Blog.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Blog.Domain.Services
{
    public class PostService
    {
        private ApplicationDbContext DbContext { get; set; }
        public PostService(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public Post AddPost(Post post)
        {
            DbContext.Posts.Add(post);

            DbContext.SaveChanges();

            return post;
        }

        public Post Find(int id)
        {
            return DbContext.Posts
                .Include(p => p.User)
                .Include(p => p.PostTags)
                    .ThenInclude(pt => pt.Tag)
                .SingleOrDefault(p => p.Id == id);
        }

        public Post UpdatePost(Post post)
        {
            DbContext.Posts.Update(post);

            DbContext.SaveChanges();

            return post;
        }
    }
}
