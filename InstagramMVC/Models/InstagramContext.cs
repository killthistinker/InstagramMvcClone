using InstagramMVC.ViewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace InstagramMVC.Models
{
    public class InstagramContext : IdentityDbContext<User, Role, int>
    {
        public DbSet<Publication> Publications { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public InstagramContext(DbContextOptions<InstagramContext> options) : base(options)
        {
            
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}