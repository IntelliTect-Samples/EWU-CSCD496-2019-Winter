using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace src.Models
{
    public class Group : Entity
    {
        public string Title { get; set; }
        public Collection<User> UsersPartOfGroup { get; set; }

        public Group(string title, Collection<User> usersPartOfGroup)
        {
            Title = title;
            UsersPartOfGroup = usersPartOfGroup;
        }
    }
}
