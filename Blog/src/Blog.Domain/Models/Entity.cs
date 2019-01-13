using Blog.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Domain.Models
{
    public class Entity : IEntity
    {
        public int Id { get; set; }
    }
}
