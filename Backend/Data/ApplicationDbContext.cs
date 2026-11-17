
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

        // Module 1: Food Items table
        public DbSet<FoodItem> FoodItems { get; set; }

        // Module 2: Categories table
        public DbSet<Category> Categories { get; set; }

        // Configure database model properties
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure FoodItem Price precision
            modelBuilder.Entity<FoodItem>()
                .Property(f => f.Price)
                .HasPrecision(10, 2);

            // Configure Category Name
            modelBuilder.Entity<Category>()
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            // Configure Category Description
            modelBuilder.Entity<Category>()
                .Property(c => c.Description)
                .HasMaxLength(250);

            // Default category status
            modelBuilder.Entity<Category>()
                .Property(c => c.IsActive)
                .HasDefaultValue(true);

            // Prevent duplicate category names
            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Name)
                .IsUnique();
        }
    }
}
