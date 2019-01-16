using System.Collections.Generic;

namespace SecretSanta.Domain
{
    public class User
    {
        string FirstName { set; get; }
        string LastName { set; get; }
        List<Gift> UserGifts { set; get; }
        List<Group> UserGroups { set; get; }

        public User(string firstName, string lastName, Gift gift = null, Group group = null)
        {
            FirstName = firstName;
            LastName = lastName;
            UserGifts = new List<Gift>();
            UserGroups = new List<Group>();

            if (gift != null)
                UserGifts.Add(gift);
            if (group != null)
                UserGroups.Add(group);
        }

    }
}