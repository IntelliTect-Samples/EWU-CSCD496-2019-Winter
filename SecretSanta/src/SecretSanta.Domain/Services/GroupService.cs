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

        public void AddUser(User user, int id)
        {
            Group group = FindGroup(id);

            if(group != null)
            {
                if(!group.Users.Contains(user))
                {
                    group.Users.Add(user);
                }
                DbContext.Groups.Update(group);
                DbContext.SaveChanges();
            }
        }

        public void RemoveUser(User user, int id)
        {
            Group group = FindGroup(id);

            if(group != null)
            {
                if(group.Users.Contains(user))
                {
                    group.Users.Remove(user);
                }
                DbContext.Groups.Update(group);
                DbContext.SaveChanges();
            }
        }

        public bool HasUser(User user, int id)
        {
            bool test = false;

            Group group = FindGroup(id);

            test = group.Users.Contains(user);

            return test;
        }

        private Group FindGroup(int id)
        {
            return DbContext.Groups.Find(id);
        }
    }
}
