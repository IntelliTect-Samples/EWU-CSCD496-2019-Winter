﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public static List<Domain.Models.Group> ToListDomain(List<DTO.Group> group)
        {
            var listGroupDomain = new List<Domain.Models.Group>();
            for ( int i = 0; i < group.Count; i++)
                listGroupDomain.Add(new Domain.Models.Group { Id = group[i].Id, Name = group[i].Name });
            return listGroupDomain;
        }

    }
}
