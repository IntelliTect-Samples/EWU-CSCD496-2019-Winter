using SecretSanta.Domain.Interfaces;

namespace SecretSanta.Domain.Models
{
    public class Entity : IEntity
    {
        public int Id { get; set; }
        public string EntityType { get; set; }
    }
}
