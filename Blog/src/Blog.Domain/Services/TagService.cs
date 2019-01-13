using Blog.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Domain.Services
{
    public class TagService
    {
        private ApplicationDbContext DbContext { get; }
        public TagService(ApplicationDbContext context)
        {
            DbContext = context;
        }
    }
}
