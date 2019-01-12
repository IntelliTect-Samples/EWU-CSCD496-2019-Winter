using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Models
{
    public class User
    {
        public string first { get; set; }
        public string last { get; set; }
        public ICollection<Group> GroupList { get; set; }
        public ICollection<Gift> ListOfGifts { get; set; }
        
        public User(string fName, string lName)
        {
            first = fName;
            last = lName;
        }
    }
}
