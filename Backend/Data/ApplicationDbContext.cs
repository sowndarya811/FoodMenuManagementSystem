
using Microsoft.EntityFrameworkCore;
using Food_Menu_Management_System.Models;

namespace Food_Menu_Management_System.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Constructor
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // FoodItems database table
        public DbSet<FoodItem> FoodItems { get; set; }

        // Configure database model properties
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Price column precision
            modelBuilder.Entity<FoodItem>()
                .Property(f => f.Price)
                .HasPrecision(10, 2);
        }
    }
}
