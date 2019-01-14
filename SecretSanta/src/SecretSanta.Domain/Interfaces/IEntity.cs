using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Interfaces
{
    public interface IEntity
    {
        int ID { get; set; }
        string EntityType { get; set; }
    }
}
