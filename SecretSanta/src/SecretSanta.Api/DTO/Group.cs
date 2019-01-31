using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.Api.DTO
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Group()
        {

        }

        public Group(SecretSanta.Domain.Models.Group modelsGroup)
        {
            if (modelsGroup == null) throw new ArgumentNullException(nameof(modelsGroup));

            Id = modelsGroup.Id;
            Name = modelsGroup.Name;
        }

        public static SecretSanta.Domain.Models.Group ToEntity(DTO.Group dtoGroup)
        {
            if (dtoGroup == null)
            {
                throw new ArgumentNullException(nameof(dtoGroup));
            }
            Domain.Models.Group entity = new Domain.Models.Group//same arguements as constructor
            {
                Id = dtoGroup.Id,
                Name = dtoGroup.Name
            };

            return entity;
        }
    }
}
