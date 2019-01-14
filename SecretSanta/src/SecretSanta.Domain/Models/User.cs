using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Models
{
    public class User : Entity
    {
        public string first { get; set; }
        public string last { get; set; }
        public ICollection<Group> GroupList { get; set; }
        public ICollection<Gift> ListOfGifts { get; set; }
        
        public User()
        {
            EntityType = "User";
        }
    }
}
