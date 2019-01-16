using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Models
{
    public class User
    {
        string FirstName { set; get; }
        string LastName { set; get; }
        List<Gift> UserGifts { set; get; }
        //commented for basic testing until many-to-many connection is made
        //List<Group> UserGroups { set; get; }

    }
}
