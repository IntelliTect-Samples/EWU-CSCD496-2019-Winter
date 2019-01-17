using System;
using System.Collections.Generic;
using System.Text;

namespace src.Model
{
    public class User : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Gift> GiftList { get; set; }
    }
}
