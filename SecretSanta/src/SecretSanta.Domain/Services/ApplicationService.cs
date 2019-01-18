using Microsoft.EntityFrameworkCore;
using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Services
{
    public class ApplicationService
    {
        protected ApplicationDbContext DbContext { get; set; }
        public ApplicationService(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }
    }
}
