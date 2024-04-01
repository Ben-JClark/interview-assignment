using Microsoft.EntityFrameworkCore;
using SeekaProject.Server.Models;

namespace SeekaProject.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options ): base( options )
        {
        }

        public DbSet<SeekaProject.Server.Models.Product> Product { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // seeding products
            modelBuilder.Entity<SeekaProject.Server.Models.Product>().HasData(
                new Product { Id = 1, Name = "Laptop", Description = "Portable computer", Price = 999.99M },
                new Product { Id = 2, Name = "Smartphone", Description = "Handheld device", Price = 499.99M }
                );
        }
    }
}
