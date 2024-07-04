using BloggieWeb.Models.Domain;
using BloggieWeb.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BloggieWeb.Data
{
    public class BloggieDbContext : DbContext
    {
        public BloggieDbContext(DbContextOptions<BloggieDbContext> options) : base(options)
        {
        }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> Tags  { get; set; }
        public DbSet<BlogPostLike> BlogPostLike { get; set;}
        public DbSet<BlogPostComment> BlogPostComment { get; set;}
        //public DbSet<BlogPostTag> BlogPostTag { get; set; }
        public DbSet<Notifications> Notifications { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; } //added
        public DbSet<Roles> Roles { get; set; } //added
        public DbSet<Profile> Profile { get; set; } //added
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Notifications>()
        //        .HasNoKey();
        //}


    }
}
