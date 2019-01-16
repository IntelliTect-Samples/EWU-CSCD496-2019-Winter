namespace Blog.Domain.Models
{
    public class Comment : Entity
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public Post Post { get; set; }
    }
}