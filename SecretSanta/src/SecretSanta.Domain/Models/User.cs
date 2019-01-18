using System.Collections.Generic;

namespace SecretSanta.Domain.Models
{
    public class User : Entity
    {
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public List<Gift> UserGifts { set; get; }
        //public List<Group> UserGroups { set; get; }   will unstub when many-to-many relationship is implemented got future assignment
    }
}