using System.Collections.Generic;
using SecretSanta.Domain.Interfaces;
using SecretSanta.Domain.Models;

namespace SecretSanta.Domain.Services
{
    public class GroupService : IGroupService
    {
        private SecretSantaDbContext DbContext { get; }

        public GroupService(SecretSantaDbContext context)
        {
            DbContext = context;
        }

        public bool CreateGroup(string title)
        {
            Group newGroup = new Group { Title = title };

            DbContext.Groups.Add(newGroup);

            DbContext.SaveChanges();

            return true;
        }

        public bool CreateGroup(Group group)
        {
            bool test = false;

            if (DbContext.Groups.Find(group.Id) == null)
            {
                DbContext.Groups.Add(group);
                test = true;
            }
            DbContext.SaveChanges();

            return test;
        }

        public bool UpdateGroup(Group group)
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

            return true;
        }

        public bool AddUser(User user, int id)
        {
            Group group = FindGroup(id);
            UserGroup userGroup = DbContext.UserGroups.Find(user.Id, id);

            if (group != null)
            {
                if(!group.UserGroups.Contains(userGroup))
                {
                    user.UserGroups.Add(userGroup);
                    group.UserGroups.Add(userGroup);
                    DbContext.UserGroups.Add(userGroup);
                }
                DbContext.Groups.Update(group);
                DbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool RemoveUser(User user, int id)
        {
            Group group = FindGroup(id);
            UserGroup userGroup = DbContext.UserGroups.Find(user.Id, id);

            if (group != null)
            {
                if (HasUser(user.Id, id))
                {
                    DbContext.UserGroups.Remove(userGroup);
                    user.UserGroups.Remove(userGroup);
                    group.UserGroups.Remove(userGroup);
                }
                DbContext.Groups.Update(group);
                DbContext.SaveChanges();

                return true;
            }

            return false;
        }

        public bool HasUser(User user, int id)
        {
            return HasUser(user.Id, id);
        }

        public bool HasUser(int uid, int gid)
        {
            bool test = false;

            if (DbContext.UserGroups.Find(uid, gid) != null)
                test = true;

            return test;
        }

        public Group FindGroup(int id)
        {
            return DbContext.Groups.Find(id);
        }

        public List<User> GetUsersFromGroup(int id)
        {
            List<User> list = new List<User>();

            var users = DbContext.Users;
            var userGroup = DbContext.UserGroups;

            foreach (UserGroup ug in userGroup)
            {
                foreach (User u in users)
                {
                    if(ug.UserId == u.Id && ug.GroupId == id)
                    {
                        list.Add(u);
                    }
                }
            }

            return list;
        }

        public List<Group> GetAllGroups()
        {
            List<Group> list = new List<Group>();
            var groups = DbContext.Groups;

            foreach(Group g in groups)
            {
                list.Add(g);
            }

            return list;
        }

        public bool DeleteGroup(int id)
        {
            Group group = DbContext.Groups.Find(id);

            DbContext.Groups.Remove(group);
            DbContext.SaveChanges();

            return FindGroup(id) == null ? true : false;
        }

        public bool AddUser(int uid, int gid)
        {
            User user = DbContext.Users.Find(uid);

            if (user == null)
            {
                return false;
            }

            return AddUser(user, gid);
        }

        public bool RemoveUser(int uid, int gid)
        {
            User user = DbContext.Users.Find(uid);

            if (user == null)
            {
                return false;
            }

            return RemoveUser(user, gid);
        }
    }
}
