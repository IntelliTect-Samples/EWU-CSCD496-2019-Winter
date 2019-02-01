using System;

namespace SecretSanta.Api.DTO
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Group() { }

        public Group(Domain.Models.Group group)
        {
            Id = group.Id;
            Name = group.Name;
        }

        public static Domain.Models.Group ToDomain(DTO.Group group)
        {
            return new Domain.Models.Group { Id = group.Id, Name = group.Name };
        }
    }
}
