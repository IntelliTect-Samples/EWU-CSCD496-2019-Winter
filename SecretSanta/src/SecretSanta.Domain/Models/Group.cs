using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Models
{
    public class Group
    {
        string Title { set; get; }
        List<User> GroupUsers { set; get; }

    }
}
