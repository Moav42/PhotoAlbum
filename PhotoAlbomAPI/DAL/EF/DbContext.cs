using DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace DAL.EF
{
    public class DbContext : IdentityDbContext<User>
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Organisation> Organisations { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostCategories> PostCategories { get; set; }
        public DbSet<PostRate> PostRates { get; set; }
        public DbSet<PostTags> PostTags { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public DbContext() 
        {

        }

        public DbContext(DbContextOptions<DbContext> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PostCategories>().HasKey(c => new { c.CategoryId, c.PostId });
            builder.Entity<PostRate>().HasKey(c => new { c.UserId, c.PostId });
            builder.Entity<PostTags>().HasKey(c => new { c.TagId, c.PostId });

            base.OnModelCreating(builder);
        }
    }
}
