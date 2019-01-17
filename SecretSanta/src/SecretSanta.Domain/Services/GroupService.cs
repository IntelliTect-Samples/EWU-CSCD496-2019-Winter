using Microsoft.EntityFrameworkCore;
using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecretSanta.Domain.Services
{
    public class GroupService
    {
        private ApplicationDbContext DbContext { get; set; }
        public GroupService(ApplicationDbContext context)
        {
            DbContext = context;
        }

        public Group UpsertGroup(Group group)
        {
            if(group.Id == 0)
            {
                DbContext.Groups.Add(group);
            }
            else
            {
                DbContext.Groups.Update(group);
            }
            DbContext.SaveChanges();
            return group;
        }

        public Group Find(int id)
        {
            return DbContext.Groups
                .Include(g => g.UserGroups)
                    .ThenInclude(ug => ug.User)
                .SingleOrDefault(g => g.Id == id);        
        }

        public List<Group> FetchAll()
        {
            var groupTask = DbContext.Groups.ToListAsync();
            groupTask.Wait();

            return groupTask.Result;
        }
    }
}
