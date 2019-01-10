namespace Blog.Domain.Models
{
    public class Comment : Entity
    {
        public string CommentorName { get; set; }
        public string Text { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}