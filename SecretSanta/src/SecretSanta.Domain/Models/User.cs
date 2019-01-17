using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Models
{
    public class User : Entity
    {
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public List<Gift> UserGifts { set; get; }
        //commented for basic testing until many-to-many connection is made
        //public List<Group> UserGroups { set; get; }

    }
}
