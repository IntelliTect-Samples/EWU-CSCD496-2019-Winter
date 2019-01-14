using SecretSanta.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Models
{
    public class Entity : IEntity
    {
        public int ID { get; set; }
        public string EntityType { get; set; }
    }
}
