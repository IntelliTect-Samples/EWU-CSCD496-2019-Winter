using System;

namespace SecretSanta.Api.DTO
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public Group()
        {
            
        }
        
        public Group(SecretSanta.Domain.Models.Group @group)
        {
            if (group == null) throw new ArgumentNullException(nameof(group));

            Id = group.Id;
            Name = group.Name;
        }
        
        public static SecretSanta.Domain.Models.Group ToEntity(DTO.Group group)
        {
            if (group == null) throw new ArgumentNullException(nameof(group));

            return new Domain.Models.Group()
            {
                Id = group.Id,
                Name = group.Name
            };
        }
    }
}