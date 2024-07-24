using Microsoft.EntityFrameworkCore;
using PMS_Entity;

namespace PMS_DATA
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        // DbSet properties for your entities
        public DbSet<Products> Products { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ProductInventory> ProductInventory { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<FlipKartProducts> FlipKartProducts { get; set; }
        public DbSet<AmazonProducts> AmazonProducts { get; set; }

        // Other DbSets as needed

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure entity relationships, constraints, etc.
            base.OnModelCreating(modelBuilder);
        }
    }
}
