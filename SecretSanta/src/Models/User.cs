using src;
using System;
using System.Collections.ObjectModel;

namespace src.Models
{
    public class User : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Collection<Gift> GiftList { get; set; }
        public Collection<Group> GroupList { get; set; }
        public static void Main(String[] args)
        {
        }
    }
}
