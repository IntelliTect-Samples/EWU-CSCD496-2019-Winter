using System.Collections.Generic;

namespace SecretSanta.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Gift> Gifts { get; set; }
        public List<GroupUser> GroupUsers { get; set; }
    }
}