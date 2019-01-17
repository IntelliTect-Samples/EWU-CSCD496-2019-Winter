using Microsoft.EntityFrameworkCore;

namespace SecretSanta.Domain.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Gift> Gifts { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Pairing> Pairings { get; set; }
        public DbSet<Message> Messages { get; set; }
    }
}