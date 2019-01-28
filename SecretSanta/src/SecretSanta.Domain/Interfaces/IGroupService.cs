using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Interfaces
{
    public interface IGroupService
    {
        void CreateGroup(string title);
        void CreateGroup(Group group);
        void UpdateGroup(Group group);
        void AddUser(User user, int id);
        void RemoveUser(User user, int id);
        Group FindGroup(int id);
    }
}
