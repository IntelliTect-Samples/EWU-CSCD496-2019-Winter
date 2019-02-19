using System.ComponentModel.DataAnnotations;

namespace SecretSanta.Domain.Models
{
    public class Gift
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public int? OrderOfImportance { get; set; }
        public string Url { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}