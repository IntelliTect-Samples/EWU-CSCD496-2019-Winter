using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SecretSanta.Domain.Models
{
    public class Group
    {
        public int Id { get; set; }
        [Required]
        [Key]
        public string Name { get; set; }
        public List<GroupUser> GroupUsers { get; set; }
    }
}