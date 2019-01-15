using src.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace src.Services
{
    public class GroupsService
    {
        private ApplicationDbContext _db { get; }

        public GroupsService(ApplicationDbContext db)
        {
            _db = db;
        }


        private Group FindGroup(int groupId)
        {
            return _db.Groups.Find(groupId);
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
                _db.Groups.Add(group);
                var saveChanges = _db.SaveChangesAsync();
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
                _db.SaveChanges();
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
                _db.SaveChanges();
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
