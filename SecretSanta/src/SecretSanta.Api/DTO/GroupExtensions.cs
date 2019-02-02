namespace SecretSanta.Api.DTO
{
    internal static class GroupExtensions
    {
        public static Group ToDTO(this Domain.Models.Group group)
        {
            if (group == null) return null;

            return new Group
            {
                Id = group.Id,
                Name = group.Name
            };
        }

        public static Domain.Models.Group ToEntity(this Group group)
        {
            if (group == null) return null;

            return new Domain.Models.Group
            {
                Id = group.Id,
                Name = group.Name
            };
        }
    }
}