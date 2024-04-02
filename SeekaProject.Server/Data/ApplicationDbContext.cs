using Microsoft.EntityFrameworkCore;
using SeekaProject.Server.Models;

namespace SeekaProject.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options ): base( options )
        {
        }

        public DbSet<Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuring one to many relationship without navigation from Category to Product
            modelBuilder.Entity<Product>()
                .HasOne(e => e.Category)
                .WithMany()
                .HasForeignKey(e => e.CategoryId)
                .IsRequired();

            // Seeding Category and Product tables
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Technology" },
                new Category { Id = 2, Name = "Produce" },
                new Category { Id = 3, Name = "Furnature" },
                new Category { Id = 4, Name = "Gardening" }
                );
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Laptop", Description = "Portable computer", Price = 999.99M, CategoryId=1 },
                new Product { Id = 2, Name = "Smartphone", Description = "Handheld device", Price = 499.99M, CategoryId = 1 }
                );
        }
    }
}
