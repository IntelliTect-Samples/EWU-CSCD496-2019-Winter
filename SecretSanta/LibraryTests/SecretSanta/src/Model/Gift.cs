using System;
using System.Collections.Generic;
using System.Text;

namespace src.Model
{
    public class Gift : Entity, IEquatable<Gift>
    {
        public string Title { get; set; }
        public int OrderOfImportance { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }

        public bool Equals(Gift gift)
        {
            if (gift == null)
            {
                return false;
            }
            else
            {
                return gift.Title.Equals(Title);
            }
        }
    }
}
