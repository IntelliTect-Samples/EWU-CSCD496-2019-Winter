using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Services
{
    public class GroupService
    {
        private SecretSantaDbContext DbContext { get; }

        public GroupService(SecretSantaDbContext context)
        {
            DbContext = context;
        }

        public void CreateGroup(string title)
        {
            Group newGroup = new Group { Title = title };

            if(DbContext.Groups.Find(title) == null)
            {
                DbContext.Groups.Add(newGroup);
            }
            DbContext.SaveChanges();
        }

        public void AddUser(User user, string groupTitle)
        {
            Group group = FindGroup(groupTitle);

            if(group != null)
            {
                if(!group.UserList.Contains(user))
                {
                    group.UserList.Add(user);
                }
                DbContext.Groups.Update(group);
                DbContext.SaveChanges();
            }
        }

        public void RemoveUser(User user, string groupTitle)
        {
            Group group = FindGroup(groupTitle);

            if(group != null)
            {
                if(group.UserList.Contains(user))
                {
                    group.UserList.Remove(user);
                }
                DbContext.Groups.Update(group);
                DbContext.SaveChanges();
            }
        }

        private Group FindGroup(string title)
        {
            return DbContext.Groups.Find(title);
        }
    }
}
