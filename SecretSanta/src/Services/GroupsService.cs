using src.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace src.Services
{
    public class GroupsService
    {
        private ApplicationDbContext Db { get; }

        public GroupsService(ApplicationDbContext db)
        {
            Db = db;
        }


        private Group FindGroup(int groupId)
        {
            return Db.Groups.Find(groupId);
        }

        public void AddGroup(Group group)
        {
            //Group foundGroup = FindGroup(id);
            /*if (user.FirstName == null || user.LastName == null)
            {
                //do not add;
            }
            else*/
            {
                Db.Groups.Add(group);
                var saveChanges = Db.SaveChangesAsync();
                saveChanges.Wait();
            }
        }

        public void AddUser(int groupId, User user)
        {
            Group group = FindGroup(groupId);
            if (IsGroupNull(group) || user == null)
            {
                return;
            }
            else
            {
                group.UsersPartOfGroup.Add(user);
                Db.SaveChanges();
            }
        }

        public void RemoveUser(int groupId, User user)
        {
            Group group = FindGroup(groupId);
            if (IsGroupNull(group) || IsUserNull(user))
            {
                return;
            }
            else
            {
                group.UsersPartOfGroup.Remove(user);
                Db.SaveChanges();
            }
        }

        private bool IsUserNull(User user)
        {
            return user == null;
        }

        private bool IsGroupNull(Group group)
        {
            return group == null;
        }
    }
}
