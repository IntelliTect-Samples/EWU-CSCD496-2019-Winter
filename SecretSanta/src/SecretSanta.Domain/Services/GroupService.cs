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

            DbContext.Groups.Add(newGroup);

            DbContext.SaveChanges();
        }

        public void CreateGroup(Group group)
        {
            if (DbContext.Groups.Find(group.Id) == null)
            {
                DbContext.Groups.Add(group);
            }
            DbContext.SaveChanges();
        }

        public void UpdateGroup(Group group)
        {
            if(group.Id == default(int))
            {
                DbContext.Groups.Add(group);
            }
            else
            {
                DbContext.Groups.Update(group);
            }
            DbContext.SaveChanges();
        }

        public void AddUser(User user, int id)
        {
            Group group = FindGroup(id);
            UserGroup userGroup = new UserGroup() { Group = group, GroupId = group.Id, User = user, UserId = user.Id };

            if (group != null)
            {
                if(!group.UserGroups.Contains(userGroup))
                {
                    group.UserGroups.Add(userGroup);
                    DbContext.UserGroups.Add(userGroup);
                }
                DbContext.Groups.Update(group);
                DbContext.SaveChanges();
            }
        }

        public void RemoveUser(User user, int id)
        {
            Group group = FindGroup(id);
            UserGroup userGroup = new UserGroup() { Group = group, GroupId = group.Id, User = user, UserId = user.Id };

            if(group != null)
            {
                if(group.UserGroups.Contains(userGroup))
                {
                    group.UserGroups.Remove(userGroup);
                    DbContext.UserGroups.Remove(userGroup);
                }
                DbContext.Groups.Update(group);
                DbContext.SaveChanges();
            }
        }

        public bool HasUser(User user, int id)
        {
            bool test = false;

            Group group = FindGroup(id);

            test = group.UserIsPartOf(user);
            
            return test;
        }

        public Group FindGroup(int id)
        {
            return DbContext.Groups.Find(id);
        }
    }
}
