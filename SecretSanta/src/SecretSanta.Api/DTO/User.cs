﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.Api.DTO
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public User() { }

        public User(SecretSanta.Domain.Models.User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
        }

        public static Domain.Models.User ToEntity(DTO.User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return new Domain.Models.User
            { Id = user.Id, FirstName = user.FirstName, LastName = user.LastName };
            
        }
    }
}
