using System;

namespace SecretSanta.Api.DTO
{
    public class User
    {
        public User()
        {
        }

        public User(Domain.Models.User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public static Domain.Models.User ToEntity(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            return new Domain.Models.User
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }
    }
}