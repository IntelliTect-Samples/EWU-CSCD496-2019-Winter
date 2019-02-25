using System;
using System.Collections.Generic;
using System.Text;

namespace BlogEngine.Domain.Models
{
    public class User : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
