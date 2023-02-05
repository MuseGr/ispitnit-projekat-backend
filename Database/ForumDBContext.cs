using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Database
{
    public class ForumDBContext: DbContext
    {
        public DbSet<ForumUser> ForumUsers { get; set; }
        public DbSet<Post> Posts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // AUTH

            modelBuilder.Entity<ForumUser>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasMany(e => e.PostsList).WithOne(p => p.Author);
            });
        }
        public ForumDBContext(DbContextOptions<ForumDBContext> options) : base(options) 
        {
            
        }
    }
}
