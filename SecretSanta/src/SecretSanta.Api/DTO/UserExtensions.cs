namespace SecretSanta.Api.DTO
{
    internal static class UserExtensions
    {
        public static User ToDTO(this Domain.Models.User user)
        {
            if (user == null) return null;

            return new User
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }

        public static Domain.Models.User ToEntity(this User user)
        {
            if (user == null) return null;

            return new Domain.Models.User
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }
    }
}