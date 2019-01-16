using System;
using System.Collections.Generic;
using System.Text;

namespace src.Models
{
    public class Gift : Entity
    {
        public string Title { get; set; }
        public int OrderOfImportance { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }

        /*public Gift(string title, int orderOfImportance, string url, string description, User user)
        {
            Title = title;
            OrderOfImportance = orderOfImportance;
            Url = url;
            Description = description;
            User = user;
            UserId = user.Id;
        }*/
    }
}
