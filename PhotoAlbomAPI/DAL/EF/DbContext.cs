using DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace DAL.EF
{
    /// <summary>
    /// A class for creating and configuring a database connection, inherited from IdentityDbContext
    /// </summary>
    public class DbContext : IdentityDbContext<User>
    {
        /// <summary>
        /// Represents the entities in the context
        /// </summary>
        public DbSet<Category> Categories { get; set; }
        /// <summary>
        /// Represents the entities in the context
        /// </summary>
        public DbSet<Comment> Comments { get; set; }
        /// <summary>
        /// Represents the entities in the context
        /// </summary>
        public DbSet<Organisation> Organisations { get; set; }
        /// <summary>
        /// Represents the entities in the context
        /// </summary>
        public DbSet<Post> Posts { get; set; }
        /// <summary>
        /// Represents the entities in the context
        /// </summary>
        public DbSet<PostCategories> PostCategories { get; set; }
        /// <summary>
        /// Represents the entities in the context
        /// </summary>
        public DbSet<PostRate> PostRates { get; set; }
        /// <summary>
        /// Represents the entities in the context
        /// </summary>
        public DbSet<PostTags> PostTags { get; set; }
        /// <summary>
        /// Represents the entities in the context
        /// </summary>
        public DbSet<Tag> Tags { get; set; }
        /// <summary>
        ///    Default ctor
        /// </summary>
        public DbContext() 
        {

        }
        /// <summary>
        /// Constructor accepting DbContextOptions, configured using dependency injection
        /// </summary>
        /// <param name="options">Configures the context with the data provider and connection string</param>
        public DbContext(DbContextOptions<DbContext> options) : base(options)
        {
            
        }
        /// <summary>
        /// Configures the database, uses DI
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
        /// <summary>
        /// Configure schema of the database relations
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PostCategories>().HasKey(c => new { c.CategoryId, c.PostId });
            builder.Entity<PostRate>().HasKey(c => new { c.UserId, c.PostId });
            builder.Entity<PostTags>().HasKey(c => new { c.TagId, c.PostId });

            base.OnModelCreating(builder);
        }
    }
}
