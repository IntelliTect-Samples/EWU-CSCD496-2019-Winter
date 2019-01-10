namespace Blog.Domain.Models
{
    public class PostTag : Entity
    {
        public int PostId { get; set; }
        public Post Post { get; set; }
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}