using Microsoft.EntityFrameworkCore;
using backend.Model;

namespace backend.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; } 
        public DbSet<Creator> Creators { get; set; } 
        public DbSet<Post> Posts { get; set; } 
        public DbSet<Comment> Comments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(c => c.Creator)
                .WithOne(u => u.User)
                .HasForeignKey<Creator>(c => c.UserId);

            modelBuilder.Entity<Post>()
                .HasOne(p => p.Creator)
                .WithMany(s => s.Posts)
                .HasForeignKey(p => p.CreatorId);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Creator)
                .WithMany(s => s.Comments)
                .HasForeignKey(c => c.CreatorId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

