using Microsoft.EntityFrameworkCore;
using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Services
{
    public class PostService
    {
        private ApplicationDbContext DbContext { get; }
        public PostService(ApplicationDbContext context)
        {
            DbContext = context;
        }

        public void UpsertPost(Post post)
        {
            if (post.Id == default(int))
            {
                DbContext.Posts.Add(post);
            }
            else
            {
                DbContext.Posts.Update(post);
            }

            var saveChangesTask = DbContext.SaveChangesAsync();
            saveChangesTask.Wait();
        }

        public void DeletePost(int id)
        {
            var postToDelete = Find(id);
            DbContext.Posts.Remove(postToDelete);

            var saveChangesTask = DbContext.SaveChangesAsync();
            saveChangesTask.Wait();
        }

        public Post Find(int id)
        {
            var findTask = DbContext.Posts.FindAsync(id);
            findTask.Wait();

            return findTask.Result;
        }

        public List<Post> FetchAll()
        {
            var postTask = DbContext.Posts.ToListAsync();
            postTask.Wait();

            return postTask.Result;
        }
    }
}
