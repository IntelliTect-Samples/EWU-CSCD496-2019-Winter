using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Models
{
    class User
    {
        string FirstName { set; get; }
        string LastName { set; get; }
        List<Gift> UserGifts { set; get; }
        List<Group> UserGroups { set; get; }



    }
}
