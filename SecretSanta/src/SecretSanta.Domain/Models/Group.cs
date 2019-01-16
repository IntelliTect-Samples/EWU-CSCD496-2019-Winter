using System.Collections.Generic;

namespace SecretSanta.Domain
{
    public class Group
    {
        string Title { set; get; }
        List<User> Members { set; get; }

        public Group(string title)
        {
            Title = title;
            Members = new List<User>();
        }

 
    }
}