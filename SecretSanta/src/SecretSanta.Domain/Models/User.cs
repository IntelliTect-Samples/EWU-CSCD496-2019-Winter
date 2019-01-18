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
        public List<UserGroup> UserGroups { set; get; }

    }
}
