using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Models
{
    class Group
    {
        string Title { set; get; }
        List<User> GroupUsers { set; get; }

    }
}
