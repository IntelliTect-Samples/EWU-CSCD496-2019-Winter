using BlogEngine.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogEngine.Domain.Models
{
    public class Entity : IEntity
    {
        public int Id { get; set; }
    }
}
