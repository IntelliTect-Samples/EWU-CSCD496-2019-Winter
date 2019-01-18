using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Gift> Gift { get; set; }
        public DbSet<Pairing> Parings { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserGroup>().HasKey(ug => new { ug.UserId, ug.GroupId });

            modelBuilder.Entity<UserGroup>()
               .HasOne(pt => pt.User)
               .WithMany(p => p.UserGroups)
               .HasForeignKey(pt => pt.UserId);

            modelBuilder.Entity<UserGroup>()
                .HasOne(pt => pt.Group)
                .WithMany(t => t.UserGroups)
                .HasForeignKey(pt => pt.GroupId);
        }
    }
}
