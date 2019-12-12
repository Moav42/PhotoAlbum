using System;
using System.Collections.Generic;
using System.Text;
using DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF
{
    class DbContext : IdentityDbContext<User>
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
            string connectionString = @"Data Source = LAPTOP-BPN6QIHG\SQLEXPRESS666; database = PhotoAlbom; integrated security = True; MultipleActiveResultSets = True; App = EntityFramework;";
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    connectionString,
                    options => options.EnableRetryOnFailure());
            }
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
