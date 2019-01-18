namespace SecretSanta.Domain.Models
{
    public class Gift : Entity
    {
        public string Title { set; get; }
        public string URL { set; get; }
        public string Description { set; get; }
        public int OrderOfImportance { set; get; }
        public User User { set; get; }
    }
}